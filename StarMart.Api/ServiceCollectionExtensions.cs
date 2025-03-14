using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarMart.Application;
using StarMart.Infrastructure;

namespace StarMart.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsInApplication(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            return services;
        }

        public static IServiceCollection InitializeServices(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("DbConnection");

            services.RegisterDbContext(dbConnectionString);
            services.RegisterRepositories();
            services.AddCqrs();

            return services;
        }
    }
}
