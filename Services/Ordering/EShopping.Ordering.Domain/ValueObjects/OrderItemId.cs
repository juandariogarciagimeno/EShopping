namespace EShopping.Ordering.Domain.ValueObjects;

public readonly record struct OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid v) => Value = v;

    public static OrderItemId New()
    {
        return new OrderItemId(Guid.NewGuid());
    }

    public static OrderItemId Of(Guid g)
    {
        DomainException.ThrowIfEmpty<OrderItemId>(g);

        return new OrderItemId(g);
    }
}