namespace EShopping.Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext context) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id);
    }

    private static Order CreateNewOrder(OrderDto dto)
    {
        var shippingAddress = dto.ShippingAddress.ToAddress();
        var billingAddress = dto.BillingAddress.ToAddress();
        var payment = dto.Payment.ToPayment();

        var order = Order.Create(
            OrderId.New(),
            CustomerId.Of(dto.CustomerId),
            OrderName.Of(dto.OrderName),
            shippingAddress,
            billingAddress,
            payment);

        foreach (var item in dto.OrderItems)
        {
            order.Add(
                ProductId.Of(item.ProductId),
                item.Quantity,
                item.Price);
        }

        return order;
    }
}
