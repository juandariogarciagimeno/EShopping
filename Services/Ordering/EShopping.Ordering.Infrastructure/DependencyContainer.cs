using EShopping.Ordering.Application.Data;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EShopping.Ordering.Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DomainEventDispatcherInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp,opts) =>
            {
                opts.UseSqlServer(config.GetConnectionString("Database")!);
                opts.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddHealthChecks()
                .AddSqlServer(config.GetConnectionString("Database")!);

            return services;
        }

        public static async Task UseMigrations(this IHost app, IHostEnvironment env)
        {
            var scope = app.Services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();

            if (env.IsDevelopment())
            {
                await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().SeedData();
            }
        }
    }
}
