using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShopping.Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(id => id.Value, id => OrderId.Of(id));

        builder.Property(c => c.CustomerId).HasConversion(id => id.Value, value => CustomerId.Of(value));

        builder.ComplexProperty(o => o.OrderName, nameBuilder =>
        {
            nameBuilder
                .Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAdress, AddressComplexBuilder);
        builder.ComplexProperty(o => o.BillingAdress, AddressComplexBuilder);
        builder.ComplexProperty(o => o.Payment, PaymenComplexBuider);

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                s => Enum.Parse<OrderStatus>(s));

        builder
            .HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired();
    }

    private Action<ComplexPropertyBuilder<Address>> AddressComplexBuilder = addressBuilder =>
    {
        addressBuilder
            .Property(a => a.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        addressBuilder.Property(a => a.LastName)
            .HasMaxLength(50)
            .IsRequired();

        addressBuilder.Property(a => a.EmailAddress)
            .HasMaxLength(50)
            .IsRequired();


        addressBuilder.Property(a => a.AddressLine)
            .HasMaxLength(180)
            .IsRequired();

        addressBuilder.Property(a => a.Country)
            .HasMaxLength(50)
            .IsRequired();

        addressBuilder.Property(a => a.State)
            .HasMaxLength(50)
            .IsRequired();

        addressBuilder.Property(a => a.ZipCode)
            .HasMaxLength(5)
            .IsRequired();
    };

    private Action<ComplexPropertyBuilder<Payment>> PaymenComplexBuider = paymentBuilder =>
    {
        paymentBuilder.Property(p => p.CardName)
            .HasMaxLength(50);

        paymentBuilder.Property(p => p.CardNumber)
            .HasMaxLength(24)
            .IsRequired();

        paymentBuilder.Property(p => p.Expiration)
            .HasMaxLength(10);

        paymentBuilder.Property(p => p.CVV)
            .HasMaxLength(3);

        paymentBuilder.Property(p => p.PaymentMethod);
    };
}
