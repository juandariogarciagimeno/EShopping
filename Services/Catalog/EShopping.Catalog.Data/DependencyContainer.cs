using EShopping.Catalog.Data.Interfaces;
using EShopping.Catalog.Data.Repositories;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EShopping.Catalog.Data
{
    public static class DependencyContainer
    {
        public static IHostApplicationBuilder AddDataAccess(this IHostApplicationBuilder builder, IConfiguration config)
        {
            builder.Services.AddMarten(opts =>
            {
                opts.Connection(config.GetConnectionString("CatalogDb")!);
                opts.DisableNpgsqlLogging = true;
            }).UseLightweightSessions();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.InitializeMartenWith<SeedData>();
            }

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            return builder;
        }

        public static IServiceCollection AddDataAccessHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            services
            .AddHealthChecks()
            .AddNpgSql(config.GetConnectionString("CatalogDb")!);

            return services;
        }
    }
}
