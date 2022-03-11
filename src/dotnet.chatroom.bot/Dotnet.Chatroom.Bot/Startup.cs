using Dotnet.Chatroom.Bot.Repository;
using Dotnet.Chatroom.Bot.Service;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
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
		}
	}
}
