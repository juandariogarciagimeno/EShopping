using EShopping.Ordering.Application.Orders.EventHandlers.Integration;
using EShopping.Shared.BuildingBlocks.Behaviours;
using EShopping.Shared.BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace EShopping.Ordering.Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehahviour<,>));
            });
            services.AddMessageBroker(config, Assembly.GetExecutingAssembly());
            services.AddFeatureManagement();

            return services;
        }
    }
}
