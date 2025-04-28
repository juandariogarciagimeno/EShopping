using Microsoft.Extensions.DependencyInjection;
using EShopping.Shared.BuildingBlocks.Behaviours;
using Grpc.Net.Client;
using static EShopping.Discount.Grpc.Discount;
using Microsoft.Extensions.Configuration;


namespace EShopping.Basket.Features
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddCarter(new DependencyContextAssemblyCatalog([typeof(DependencyContainer).Assembly]))
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(DependencyContainer).Assembly);
                    cfg.AddOpenBehavior(typeof(ValidationBehahviour<,>));
                    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
                })
                .AddValidatorsFromAssembly(typeof(DependencyContainer).Assembly)
                .AddGrpcClient<DiscountClient>(options =>
                {
                    options.Address = Uri.TryCreate(config.GetConnectionString("Discount"), UriKind.Absolute, out Uri? uri) ? uri : throw new Exception("Invalid discount service uri");
                });

            return services;
        }

        public static WebApplication MapFeatures(this WebApplication app)
        {
            app.MapCarter();

            return app;
        }
    }
}
