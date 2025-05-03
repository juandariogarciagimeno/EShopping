namespace EShopping.Ordering.Domain.ValueObjects;

public readonly record struct ProductId
{
    public Guid Value { get; }

    private ProductId(Guid v) => Value = v;

    public static ProductId New()
    {
        return new ProductId(Guid.NewGuid());
    }

    public static ProductId Of(Guid g)
    {
        DomainException.ThrowIfEmpty<ProductId>(g);

        return new ProductId(g);
    }

    public static implicit operator Guid(ProductId id)
    {
        return id.Value;
    }
}