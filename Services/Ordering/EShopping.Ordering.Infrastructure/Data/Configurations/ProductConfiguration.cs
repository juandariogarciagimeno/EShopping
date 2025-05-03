using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShopping.Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id).HasConversion(id => id.Value, value => ProductId.Of(value));

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}