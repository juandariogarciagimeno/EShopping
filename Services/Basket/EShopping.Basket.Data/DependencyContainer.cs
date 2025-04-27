using EShopping.Basket.Data.Interfaces;
using EShopping.Basket.Data.Models;
using EShopping.Basket.Data.Repository;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopping.Basket.Data;

public static class DependencyContainer
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
    {
        services.AddMarten(opts =>
        {
            opts.Connection(config.GetConnectionString("BasketDb")!);
            opts.DisableNpgsqlLogging = true;
            opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
        }).UseLightweightSessions();

        services.AddScoped<IBasketRepository, BasketRepository>();
        services.Decorate<IBasketRepository, CachedBasketRepository>();

        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = config.GetConnectionString("Redis")!;
            opt.InstanceName = "default";
        });

        return services;
    }

    public static IServiceCollection AddDataAccessHealthChecks(this IServiceCollection services, IConfiguration config)
    {
        services
        .AddHealthChecks()
        .AddNpgSql(config.GetConnectionString("BasketDb")!)
        .AddRedis(config.GetConnectionString("Redis")!);

        return services;
    }
}
