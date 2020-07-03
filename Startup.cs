using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using B.API.Entities;
using B.API.Database;
using Geocoding.Microsoft;
using B.API.Models;
using B.API.AutoMapper;

using AutoMapper;

namespace B.API
{
    public class Startup
    {

        // hosting environment points to the root of our app
        // specify specific environments after default env
        public Startup(IWebHostEnvironment env)
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

        public IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            // services.AddMvc(options =>
            //     {
            //         options.RespectBrowserAcceptHeader = true; // false by default
            // });
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(typeof(MappingProfile));


            services.AddScoped(sp => new DarkSky.Services.DarkSkyService(Configuration["Weather:ServiceApiKey"]));
            services.AddScoped(sp => new BingMapsGeocoder(Configuration["Maps:ServiceApiKey"]));
            services.AddScoped<BookRepository>();
            services.AddScoped<TransactionRepository>();
            services.AddScoped<LookupRepository>();
            services.AddScoped<FinanceRepository>();
            services.AddScoped<BlogPostRepository>();

            // SOURCED FROM https://www.scottbrady91.com/Entity-Framework/Entity-Framework-Core-In-Memory-Testing
            // Building the connection string is necessary to avoid this error when publishing:
            // Format of the initialization string does not conform to specification starting at index 0
            var connectionStringBuilder = new SqliteConnectionStringBuilder{ 
                DataSource= Configuration.GetConnectionString(Environment.EnvironmentName)
            };

            var connectionString = connectionStringBuilder.ToString();
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));

            // IMPORTANT - AppDatabase Context is using new database
            var appApiConnectionStringBuilder = new SqliteConnectionStringBuilder{ 
                DataSource = Configuration.GetSection("Database")[Environment.EnvironmentName]
            };
            var appApiConnectionString = appApiConnectionStringBuilder.ToString();
            services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>(options => options.UseSqlite(appApiConnectionString));



            string domain = $"https://{Configuration["authentication:Domain"]}/";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["authentication:ApiIdentifier"];
            });

            services.AddOpenApiDocument(config => 
                config.PostProcess = document => {
                    document.Info.Title = "B API";
                    document.Info.Version = "v1";
                    document.Info.Description = "";

                }
            ); // add OpenAPI v3 document

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // References launch.jon ASPNETCORE_Environment
            if (env.EnvironmentName != "prod")
            {
                // display detailed errors for dev 
                app.UseDeveloperExceptionPage();
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


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}