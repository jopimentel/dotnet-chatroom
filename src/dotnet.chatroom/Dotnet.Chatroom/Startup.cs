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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{AssemblyVersion}", new OpenApiInfo { Title = "Dotnet.Chatroom", Version = $"v{AssemblyVersion}" });
            });

            services.AddSignalR();
            services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // TODO: Take from a environment variable
            string connection = "Server=localhost,1434; Database=master; Integrated Security=False; User Id=sa; Password=MSSQL@admin1;";
#if DEBUG
            services.AddDbContext<ChatroomContext>(options => options.UseLoggerFactory(loggerFactory).UseSqlServer(connection));
#else
            services.AddDbContext<ChatroomContext>(options => options.UseSqlServer(connection));
#endif

            RegisterDependencies(services);

            // TODO: Take from a environment variable
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{AssemblyVersion}/swagger.json", $"Dotnet.Chatroom v{AssemblyVersion}"));
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
            services.AddHttpContextAccessor();
            //services.AddScoped<IApplicationContext, HttpApplicationContext>();

            // Dependencies goes here
        }

    }
}
