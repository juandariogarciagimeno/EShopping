using EShopping.Shared.BuildingBlocks.Exceptions.Handler;
using EShopping.Shared.Utils;
using EShopping.Shared.Utils.Tracing;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace EShopping.Ordering.Api
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddOpenApi()
                .AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]))
                .AddExceptionHandler<ExceptionHandler>()
                .AddTraceLogger()
                .AddHealthChecks();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.UseExceptionHandler(opts => { });
            app.UseTraceLogger();
            app.MapCarter();
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            
            return app;
        }
    }
}