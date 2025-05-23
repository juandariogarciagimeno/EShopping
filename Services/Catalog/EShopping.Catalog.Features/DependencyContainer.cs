﻿using EShopping.Shared.BuildingBlocks.Behaviours;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EShopping.Catalog.Features
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            return services
                .AddCarter(new DependencyContextAssemblyCatalog([typeof(DependencyContainer).Assembly]))
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(DependencyContainer).Assembly);
                    cfg.AddOpenBehavior(typeof(ValidationBehahviour<,>));
                    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
                })
                .AddValidatorsFromAssembly(typeof(DependencyContainer).Assembly);
        }

        public static WebApplication MapFeatures(this WebApplication app)
        {
            app.MapCarter();

            return app;
        }
    }
}
