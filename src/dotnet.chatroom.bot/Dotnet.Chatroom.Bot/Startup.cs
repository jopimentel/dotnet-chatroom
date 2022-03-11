using Dotnet.Chatroom.Bot.Repository;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dotnet.Chatroom.Bot
{
	public class Startup
	{
		public string AssemblyVersion => GetType().Assembly.GetName().Version.ToString();
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc($"v{AssemblyVersion}", new OpenApiInfo { Title = "Dotnet.Chatroom.Bot", Version = $"v{AssemblyVersion}" });
			});

			services.AddSignalR();
			services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
			services.AddLogging(configure => configure.AddConsole());

			RegisterDependencies(services);

			// TODO: Take from a environment variable and configure endpoints
			services.AddCors(policy => policy.AddPolicy("anywhere", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
			services.AddCors(policy => policy.AddPolicy("production", p => p.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{AssemblyVersion}/swagger.json", $"Dotnet.Chatroom.Bot v{AssemblyVersion}"));
			}

			app.UseRouting();
			app.UseCors();
			app.UseAuthorization();
			app.UseAuthentication();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		private void RegisterDependencies(IServiceCollection services)
		{
			// TODO: Add ApplicationContext
			//services.AddHttpContextAccessor();
			//services.AddScoped<IApplicationContext, ApplicationContext>();

			// MongoDB
			// TODO: Create generics for mongodb
			services.AddSingleton<IMongoClient>(_ =>
			{
				return new MongoClient("mongodb://sysadmin:mongodb%401@localhost:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
			});

			services.AddSingleton(provider =>
			{
				IMongoClient client = provider.GetRequiredService<IMongoClient>();
				return client?.GetDatabase("files");
			});

			services.AddScoped<IFileRepository, FileRepository>();

			// RabbitMQ
			ConnectionFactory factory = new()
			{
				Uri = new Uri("amqp://sysadmin:rabbitmq%401@localhost:5672/chatroom")
			};

			services.AddSingleton(_ => factory.CreateConnection());
			services.AddScoped(provider =>
			{
				IConnection connection = provider.GetRequiredService<IConnection>();
				IModel model = connection.CreateModel();

				model.QueueDeclare("bot::stock.quote.out", durable: true, exclusive: false, autoDelete: false);

				return model;
			});

			IEnumerable<Type> types = GetType().Assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IHandler).IsAssignableFrom(t));

			foreach (Type type in types)
				services.AddSingleton(type);

			ServiceProvider provider = services.BuildServiceProvider();
			IConnection connection = provider.GetRequiredService<IConnection>();

			foreach (Type type in types)
			{
				Type[] interfaces = type.GetInterfaces();
				Type @interface = interfaces.Where(i => i.GenericTypeArguments.Length == 1).FirstOrDefault();
				Type genericType = @interface.GenericTypeArguments.FirstOrDefault();
				object instance = provider.GetService(type);
				IHandler handler = (IHandler)instance;
				IModel model = connection.CreateModel();

				model.QueueDeclare(handler.Queue, durable: true, exclusive: false, autoDelete: false);

				EventingBasicConsumer consumer = new(model);

				consumer.Received += (object sender, BasicDeliverEventArgs arguments) =>
				{
					try
					{
						byte[] message = arguments.Body.ToArray();
						string body = Encoding.UTF8.GetString(message);
						object data = JsonSerializer.Deserialize(body, genericType);
						MethodInfo method = type.GetMethod("HandleAsync");

						method.Invoke(instance, parameters: new[] { data, model, arguments });
					}
					catch (Exception) { }
				};

				model.BasicConsume(handler.Queue, autoAck: false, consumer);
			}
		}
	}
}
