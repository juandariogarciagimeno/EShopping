using EShopping.Discount.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EShopping.Discount.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
        [
            new Coupon() { Id = 1, ProductName = "IPhone X", Description = "IPhone X Discount", Amount = 150},
            new Coupon() { Id = 2, ProductName = "Samung 10", Description = "Samsung 10 Discount", Amount = 100},
        ]);
    }
}

public class RuntimeDBContextFactory : IDesignTimeDbContextFactory<DiscountContext>
{
    public DiscountContext CreateDbContext(string[] args = null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DiscountContext>();
        optionsBuilder.UseSqlite("Data Source=discountdb.db");
        return new DiscountContext(optionsBuilder.Options);
    }
}
