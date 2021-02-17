using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using b.Api.Entities;
using b.Api.Database;
using Geocoding.Microsoft;
using b.Api.AutoMapper;

using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Rewrite;
using b.Data.Crypto.Read;
using b.Entities;
using b.Domain.Crypto;
using b.Domain.AutoMapper;
using b.Data.AutoMapper;
using System.Reflection;
using b.Data.Crypto.Write;
using System.Linq;
using System;
using b.Data.Models;

namespace b.Api
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
                .AddJsonFile("secrets/appsettings.secrets.json", optional: true)
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
            services.AddControllers();

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dataAssembly = Assembly.LoadFrom($"{currentDirectory}\\b.Data.dll");
            services.AddAutoMapper(
                Assembly.GetExecutingAssembly(),
                dataAssembly
            );


            services.AddScoped(sp => new DarkSky.Services.DarkSkyService(Configuration["Weather:ServiceApiKey"]));
            services.AddScoped(sp => new BingMapsGeocoder(Configuration["Maps:ServiceApiKey"]));
            services.AddScoped<BookRepository>();
            services.AddScoped<TransactionRepository>();
            services.AddScoped<LookupRepository>();
            services.AddScoped<FinanceRepository>();
            services.AddScoped<BlogPostRepository>();

            // load all injectable services from data assembly (readers/writers)
            dataAssembly
              .GetTypes()
              .Where(x => (!x.IsAbstract &&
                  (typeof(IWriteDataService).IsAssignableFrom(x)
                  || typeof(IReadDataService).IsAssignableFrom(x))
                  ||
                  (x
                  .GetInterfaces()
                  .Where(i => i.IsGenericType).Any(i =>
                    i.GetGenericTypeDefinition() == typeof(IWriteAccessor<>) || i.GetGenericTypeDefinition() == typeof(IReadAccessor<,>))
                  )))
              .ToList().ForEach(t => services.AddScoped(t));

            // SOURCED FROM https://www.scottbrady91.com/Entity-Framework/Entity-Framework-Core-In-Memory-Testing
            // Building the connection string is necessary to avoid this error when publishing:
            // Format of the initialization string does not conform to specification starting at index 0
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = Configuration["BudgetDatabase"]
            };

            var connectionString = connectionStringBuilder.ToString();
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));

            // IMPORTANT - AppDatabase Context is using new database
            var appApiConnectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = Configuration["Database"]
            };
            var appApiConnectionString = appApiConnectionStringBuilder.ToString();
            services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>(options => options.UseSqlite(appApiConnectionString));



            string domain = $"https://{Configuration["Authentication:Domain"]}/";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Authentication:ApiIdentifier"];
            });

            services.AddOpenApiDocument(config =>
                config.PostProcess = document =>
                {
                    document.Info.Title = "B API";
                    document.Info.Version = "v1";
                    document.Info.Description = "";

                }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // References launch.jon ASPNETCORE_Environment
            if (env.IsDevelopment())
            {
                // display detailed errors for dev 
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors(builder =>
                builder.WithOrigins(Configuration["Clients:Bgeo"], Configuration["Clients:Budget"], Configuration["Clients:Groceries"], Configuration["Clients:Me"])
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseOpenApi(); // serve OpenAPI/Swagger documents
            app.UseSwaggerUi3(); // serve Swagger UI
            app.UseReDoc(); // serve ReDoc UI

            app.UseStatusCodePages();

            // any request prefixed /api rewrite to /
            // This is because having issues in kubernetes rewriting the proxy request in nginx from brianbrown.dev/api to /
            var options = new RewriteOptions()
                .AddRewrite("api/(.*)", "$1", skipRemainingRules: true);
            app.UseRewriter(options);


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