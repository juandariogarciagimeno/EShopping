namespace EShopping.Ordering.Domain.ValueObjects;

public readonly record struct CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid v) => Value = v;

    public static CustomerId New()
    {
        return new CustomerId(Guid.NewGuid());
    }

    public static CustomerId Of(Guid g)
    {
        DomainException.ThrowIfEmpty<CustomerId>(g);

        return new CustomerId(g);
    }

    public static implicit operator Guid(CustomerId id)
    {
        return id.Value;
    }
}