using Microsoft.AspNetCore.HttpOverrides;
using UrlShortener.Extensions;
using NLog;
using Contracts;
using UrlShortener;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configure logger
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddControllers()
.AddApplicationPart(typeof(UrlManagement.Presentation.AssemblyReference).Assembly);

var app = builder.Build();

app.UseExceptionHandler(opt => { });

// Configure the HTTP request pipeline.

if (app.Environment.IsProduction())
    // middleware for Strict-Transport-Security header
    app.UseHsts();

app.UseHttpsRedirection();
// enables using static files for the request. If we don't it will use wwwroot folder by default
app.UseStaticFiles();
// will forward proxy headers to the current request. This will help us during application deployment
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
