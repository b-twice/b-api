using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using B.API.AutoMapper;
using B.API.Database;
using Geocoding.Microsoft;
using Microsoft.Data.Sqlite;
using B.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Hosting;

var configurationBuilder = new ConfigurationBuilder();
IConfigurationRoot secrets = configurationBuilder
    .AddJsonFile("secrets/appsettings.secrets.json", optional: true)
    .AddEnvironmentVariables()
    .SetBasePath(Directory.GetCurrentDirectory())
    .Build();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers().AddNewtonsoftJson(options => 
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddScoped(sp => new DarkSky.Services.DarkSkyService(secrets["Weather:ServiceApiKey"]));
builder.Services.AddScoped(sp => new BingMapsGeocoder(secrets["Maps:ServiceApiKey"]));
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<LookupRepository>();
builder.Services.AddScoped<FinanceRepository>();
builder.Services.AddScoped<BlogPostRepository>();
builder.Services.AddScoped<FoodProductRepository>();
builder.Services.AddScoped<CookbookRepository>();
builder.Services.AddScoped<RecipeRepository>();
builder.Services.AddScoped<RecipeIngredientRepository>();
builder.Services.AddScoped<MealPlanRecipeRepository>();
builder.Services.AddScoped<MealPlanRepository>();


// SOURCED FROM https://www.scottbrady91.com/Entity-Framework/Entity-Framework-Core-In-Memory-Testing
// Building the connection string is necessary to avoid this error when publishing:
// Format of the initialization string does not conform to specification starting at index 0
// IMPORTANT - AppDatabase Context is using new database
var appApiConnectionStringBuilder = new SqliteConnectionStringBuilder{ 
    DataSource = configuration.GetValue<string>("Database")
};
var appApiConnectionString = appApiConnectionStringBuilder.ToString();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>(options => options.UseSqlite(appApiConnectionString));



string domain = $"https://{configuration.GetValue<string>("Authentication:Domain")}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = configuration.GetValue<string>("Authentication:ApiIdentifier");
});

builder.Services.AddOpenApiDocument(config => 
    config.PostProcess = document => {
        document.Info.Title = "B API";
        document.Info.Version = "v1";
        document.Info.Description = "";

    }
); 

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/error");
}

app.UseCors(b =>
    b.WithOrigins(configuration.GetValue<string>("Clients:Bgeo"), configuration.GetValue<string>("Clients:Budget"), configuration.GetValue<string>("Clients:Groceries"), configuration.GetValue<string>("Clients:Me"))
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseOpenApi(); // serve OpenAPI/Swagger documents
app.UseSwaggerUi3(); // serve Swagger UI
app.UseReDoc(); // serve ReDoc UI

app.UseStatusCodePages();

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

app.Run();