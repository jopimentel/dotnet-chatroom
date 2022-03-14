using Dotnet.Chatroom.Repository;
using Dotnet.Chatroom.Service;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Dotnet.Chatroom
{
	/// <summary>
	/// 
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Gets the version of the current assembly.
		/// </summary>
		public string AssemblyVersion => GetType().Assembly.GetName().Version.ToString();
		/// <summary>
		/// 
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			OpenApiInfo openApiInfo = new() { Title = Environment.AppName, Version = $"v{AssemblyVersion}" };

			services.AddControllers();
			services.AddSwaggerGen(c => c.SwaggerDoc($"v{AssemblyVersion}", openApiInfo));

			services.AddSignalR();
			services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
			services.AddLogging(configure => configure.AddConsole());

            services.AddDbContext<ChatroomContext>(options => options.UseSqlServer(Environment.MSSQLConnectionString));

			RegisterDependencies(services);

			services.AddCors(policy => policy.AddPolicy("cors", p => p.WithOrigins(Environment.Origins).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{AssemblyVersion}/swagger.json", $"{Environment.AppName} v{AssemblyVersion}"));
			}

			app.UseRouting();
			app.UseCors();
			app.UseAuthorization();
			app.UseAuthentication();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<ChatsHub>("hub/chats");
			});
		}

		/// <summary>
		/// Adds the application required services to the <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> instance to be used to register the services.</param>
		private void RegisterDependencies(IServiceCollection services)
		{
			services.AddEncryptor();
			services.AddApplicationContext();
			services.AddScoped(_ =>
			{
				return new HubConnectionBuilder()
					.WithUrl(Environment.MessageHub)
					.ConfigureLogging(logging => logging.AddConsole())
					.WithAutomaticReconnect()
					.Build();
			});

			// MongoDB
			services.AddMongoClient(Environment.MongoConnectionString);
			services.AddMongoDatabase<Message>(Environment.MongoDatabase);

			services.AddScoped<IChatRepository, ChatRepository>();
			services.AddScoped<IChatService, ChatService>();

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserService, UserService>();

			services.AddScoped<IMessageRepository, MessageRepository>();

			// RabbitMQ
			services
				.AddRabbitMQ(Environment.RabbitMQUri)
				.DeclareQueues(queues: new[] { Environment.StockQuoteOut })
				.AddHandlers(assembly: GetType().Assembly);
		}
	}
}
