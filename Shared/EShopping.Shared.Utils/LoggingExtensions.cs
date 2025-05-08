using EShopping.Shared.Utils.Tracing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System;
using System.Reflection;

namespace EShopping.Shared.Utils
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddTraceLogger(this IServiceCollection services) 
        {
            services.AddScoped<LoggingMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseTraceLogger(this IApplicationBuilder app)
        {           
            app.UseMiddleware<LoggingMiddleware>();
            return app;
        }

        public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] - {TraceId} - {Message:lj}{NewLine}{Exception}")
                .WriteTo.Seq(context.Configuration.GetSection("Seq:Url").Value!);
            });

            return builder;
        }

        public static IHostApplicationBuilder AddOpenTelemetry(this IHostApplicationBuilder builder)
        {
            builder.Services.AddOpenTelemetry()
                .ConfigureResource(c => c.AddService(Assembly.GetCallingAssembly().FullName!))
                .WithTracing(tracing =>
                {
                    tracing
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

                    tracing.AddOtlpExporter();
                });
            return builder;
        }
    }
}
