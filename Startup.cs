using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Budget.API.Entities;
using Budget.API.Database;

namespace Budget.API
{
    public class Startup
    {

        // hosting environment points to the root of our app
        // specify specific environments after default env
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Environment = env;
            if (env.EnvironmentName != "prod")
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();



            
        }
        public IConfigurationRoot Configuration { get; set; }

        public IHostingEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
                {
                    options.RespectBrowserAcceptHeader = true; // false by default
            });


            var darkSky = new DarkSky.Services.DarkSkyService(Configuration["Weather:ServiceApiKey"]);
            // services.AddScoped(sp => darkSky);
            services.AddScoped<BookRepository>();
            services.AddScoped<LookupRepository>();

            // SOURCED FROM https://www.scottbrady91.com/Entity-Framework/Entity-Framework-Core-In-Memory-Testing
            // Building the connection string is necessary to avoid this error when publishing:
            // Format of the initialization string does not conform to specification starting at index 0
            var connectionStringBuilder = new SqliteConnectionStringBuilder{ 
                DataSource= Configuration.GetConnectionString(Environment.EnvironmentName)
            };

            var connectionString = connectionStringBuilder.ToString();
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));

            // IMPORTANT - AppDatabase Context is using new database
            var appConnectionStringBuilder = new SqliteConnectionStringBuilder{ 
                DataSource = Configuration.GetSection("Database")[Environment.EnvironmentName]
            };
            var appConnectionString = appConnectionStringBuilder.ToString();
            services.AddEntityFrameworkSqlite().AddDbContext<AppDatabaseContext>(options => options.UseSqlite(appConnectionString));

            string domain = $"https://{Configuration["authentication:Domain"]}/";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["authentication:ApiIdentifier"];
            });

            services.AddOpenApiDocument(config => 
                config.PostProcess = document => {
                    document.Info.Title = "Mythic API";
                    document.Info.Version = "v1";
                    document.Info.Description = "";

                }
            ); // add OpenAPI v3 document

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // References launch.jon ASPNETCORE_Environment
            if (env.EnvironmentName != "prod")
            {
                // display detailed errors for dev 
                app.UseDeveloperExceptionPage();

                loggerFactory
                        .AddConsole()
                        .AddDebug();

            }


            app.UseCors(builder =>
                builder.WithOrigins(Configuration["clients:bgeo"], Configuration["clients:budget"], Configuration["clients:groceries"], Configuration["clients:me"])
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseOpenApi(); // serve OpenAPI/Swagger documents
            app.UseSwaggerUi3(); // serve Swagger UI
            app.UseReDoc(); // serve ReDoc UI

            app.UseStatusCodePages();

            app.UseAuthentication();

            // Use MVC framework to handle http requests
            app.UseMvc();

        }
    }
}