using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace EShopping.Shared.Utils
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddSerilogFromConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddLogging(logbuilder =>
            {
                var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

                logbuilder.AddSerilog(logger);
            });

            return services;
        }
    }
}
