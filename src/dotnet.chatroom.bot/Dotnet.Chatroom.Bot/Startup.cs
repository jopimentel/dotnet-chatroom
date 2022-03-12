using Dotnet.Chatroom.Bot.Repository;
using Dotnet.Chatroom.Bot.Service;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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

		public void ConfigureServices(IServiceCollection services)
		{
			OpenApiInfo openApiInfo = new () { Title = Environment.AppName, Version = $"v{AssemblyVersion}" };

			services.AddControllers();
			services.AddSwaggerGen(c => c.SwaggerDoc($"v{AssemblyVersion}", openApiInfo));

			services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
			services.AddLogging(configure => configure.AddConsole());

			RegisterDependencies(services);

			services.AddCors(policy => policy.AddPolicy("cors", p => p.WithOrigins(Environment.Origins).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
		}

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
			services.AddMongoDatabase<FileRepository>(Environment.MongoGridFSDatabase);
			services.AddMongoDatabase<Stock>(Environment.MongoDatabase);

			// Files
			services.AddScoped<IFileRepository, FileRepository>();
			services.AddScoped<IFileService, FileService>();
			// Stocks
			services.AddScoped<IStockRepository, StockRepository>();
			services.AddScoped<IStockService, StockService>();
			// Requests
			services.AddScoped<IRequestRepository, RequestRepository>();
			services.AddScoped<IRequestService, RequestService>();

			// RabbitMQ
			services
				.AddRabbitMQ(Environment.RabbitMQUri)
				.DeclareQueues(queues: new[] { Environment.StockQuoteIn, Environment.StockQuoteOut })
				.AddHandlers(assembly: GetType().Assembly);
		}
	}
}
