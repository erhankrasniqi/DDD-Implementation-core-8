using Microsoft.Extensions.DependencyInjection;
using StarMart.Application.Features.CreateCustomer;

namespace StarMart.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));

            return services;
        }
    }
}
