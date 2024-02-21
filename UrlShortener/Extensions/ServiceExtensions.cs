using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace UrlShortener.Extensions
{
    public static class ServiceExtensions
    {
        // Configure CORS
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder
                // Allow all origins
                .AllowAnyOrigin()
                // Only allow Post and Get
                .WithMethods("POST", "GET")
                .AllowAnyHeader());
            });
        
        // Enables us deploy to IIS as ASP.NET Core applications are by default self hosted
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        // services.AddSingleton method, we create a service the first time we request it
        // same instance will be used for every method call
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

    }
}
