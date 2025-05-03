namespace EShopping.Ordering.Domain.ValueObjects;

public readonly record struct OrderId
{
    public Guid Value { get; }

    private OrderId(Guid v) => Value = v;
    public static OrderId New()
    {
        return new OrderId(Guid.NewGuid());
    }

    public static OrderId Of(Guid g)
    {
        DomainException.ThrowIfEmpty<OrderId>(g);

        return new OrderId(g);
    }

    public static implicit operator Guid(OrderId id)
    {
        return id.Value;
    }
}