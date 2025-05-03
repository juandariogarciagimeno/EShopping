using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EShopping.Shared.BuildingBlocks.Messaging.MassTransit
{
    public static class MQExtensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration appconfig, Assembly? assembly = null)
        {
            services.AddMassTransit(mtcfg =>
            {
                mtcfg.SetKebabCaseEndpointNameFormatter();

                if (assembly != null)
                    mtcfg.AddConsumers(assembly);

                mtcfg.UsingRabbitMq((ctx, rmqcfg) =>
                {
                    rmqcfg.Host(new Uri(appconfig["MessageBroker:Host"]!), host =>
                    {
                        host.Username(appconfig["MessageBroker:UseerName"]!);
                        host.Password(appconfig["MessageBroker:Password"]!);
                    });

                    rmqcfg.ConfigureEndpoints(ctx);
                });
            });

            return services;
        }
    }
}
