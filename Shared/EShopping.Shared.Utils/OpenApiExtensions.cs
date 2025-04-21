using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using System.Reflection.Metadata.Ecma335;

namespace EShopping.Shared.Utils
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddOpenApiExplorer(this IServiceCollection services, IConfiguration config)
        {
            bool useScalar = config.GetValue<bool>("UseScalar", false);
            if (!useScalar)
                services.AddSwagger();

            return services;
        }

        public static WebApplication UseOpenApiExplorer(this WebApplication app, IConfiguration config)
        {
            bool useScalar = config.GetValue<bool>("UseScalar", false);
            if (!useScalar)
                app.AppUseSwagger();
            else
                app.AddScalar();

            return app;
        }

        private static WebApplication AddScalar(this WebApplication app)
        {
            app
                .MapScalarApiReference(opt =>
                {
                    opt
                    .WithTheme(ScalarTheme.DeepSpace)
                    .WithDarkMode(true)
                    .WithDarkModeToggle(false)
                    .WithDefaultHttpClient(ScalarTarget.Shell, ScalarClient.Curl);

                    opt.EnabledClients = [ScalarClient.Curl, ScalarClient.HttpClient, ScalarClient.AsyncHttp];
                    opt.EnabledTargets = [ScalarTarget.Shell, ScalarTarget.CSharp, ScalarTarget.JavaScript, ScalarTarget.PowerShell];
                    opt.HideModels = true;
                });

            return app;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();

            return services;
        }

        private static WebApplication AppUseSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
