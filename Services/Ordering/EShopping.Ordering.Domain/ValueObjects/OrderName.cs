namespace EShopping.Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 5;
    public string Value { get; }

    protected OrderName() { }
    internal OrderName(string v) => Value = v;

    public static OrderName Of(string v)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(v);
        //ArgumentOutOfRangeException.ThrowIfNotEqual(v.Length, DefaultLength);

        return new OrderName(v);
    }

    public static implicit operator string(OrderName name)
    {
        return name.Value;
    }
}