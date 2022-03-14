using Dotnet.Chatroom.Repository;
using Dotnet.Chatroom.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Dotnet.Chatroom
{
	public class Startup
	{
		public string AssemblyVersion => GetType().Assembly.GetName().Version.ToString();
		public IConfiguration Configuration { get; }

		public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			OpenApiInfo openApiInfo = new() { Title = "Dotnet.Chatroom", Version = $"v{AssemblyVersion}" };

			services.AddControllers();
			services.AddSwaggerGen(c => c.SwaggerDoc($"v{AssemblyVersion}", openApiInfo));

			services.AddSignalR();
			services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
			services.AddLogging(configure => configure.AddConsole());

#if DEBUG
			services.AddDbContext<ChatroomContext>(options => options.UseLoggerFactory(loggerFactory).UseSqlServer(Environment.MSSQLConnectionString));
#else
            services.AddDbContext<ChatroomContext>(options => options.UseSqlServer(Environment.MSSQLConnectionString));
#endif

			RegisterDependencies(services);

			services.AddCors(policy => policy.AddPolicy("cors", p => p.WithOrigins(Environment.Origins).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{AssemblyVersion}/swagger.json", $"Dotnet.Chatroom v{AssemblyVersion}"));
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
			services.AddApplicationContext();

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
