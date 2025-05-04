using EShopping.Shared.BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace EShopping.Ordering.Api
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services
                .AddOpenApi()
                .AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]))
                .AddExceptionHandler<ExceptionHandler>()
                .AddHealthChecks();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseExceptionHandler(opts => { });
            return app;
        }
    }
}