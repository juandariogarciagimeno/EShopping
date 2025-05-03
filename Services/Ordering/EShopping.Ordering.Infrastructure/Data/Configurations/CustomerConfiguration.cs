using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShopping.Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(id => id.Value,  value => CustomerId.Of(value));

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(c => c.Email).IsUnique();
    }
}
