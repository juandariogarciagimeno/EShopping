using EShopping.Shared.Utils.Tracing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace EShopping.Shared.Utils
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddTraceLogger(this IServiceCollection services, bool includeGenerator = false) 
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<TraceIdEnricher>();
            services.AddScoped<LoggingMiddleware>();

            if (includeGenerator)
                services.AddSingleton<TraceGeneratorMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseTraceLogger(this IApplicationBuilder app, bool includeGenerator = false)
        {
            if (includeGenerator)
                app.UseMiddleware<TraceGeneratorMiddleware>();

            app.UseMiddleware<LoggingMiddleware>();
            return app;
        }

        public static WebApplicationBuilder UseSerilogWithSeqSinkAndHttpEnricher(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                var enricher = services.GetRequiredService<TraceIdEnricher>();

                configuration
                .Enrich.FromLogContext()
                .Enrich.With(enricher)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] - {TraceId} - {Message:lj}{NewLine}{Exception}")
                .WriteTo.Seq(context.Configuration.GetSection("Seq:Url").Value!);
            });

            return builder;
        }
    }
}
