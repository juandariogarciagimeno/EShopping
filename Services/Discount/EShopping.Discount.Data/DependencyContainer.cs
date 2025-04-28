using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopping.Discount.Data
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DiscountContext>(options =>
                options.UseSqlite(config.GetConnectionString("DiscountDB")!));

            return services;
        }

        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            try
            {
                var scope = app.ApplicationServices.CreateScope();
                scope.ServiceProvider.GetRequiredService<DiscountContext>().Database.Migrate();
            }
            catch { }
            return app;
        }
    }
}
