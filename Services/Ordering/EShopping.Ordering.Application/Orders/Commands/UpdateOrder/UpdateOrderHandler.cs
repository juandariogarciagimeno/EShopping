namespace EShopping.Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext context) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);
        var order = await context.Orders.FindAsync([orderId], cancellationToken) ?? throw NotFoundException.Order(orderId);

        UpdateOrderWithNewValues(ref order, command.Order);

        context.Orders.Update(order);
        var res = await context.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(res > 0);
    }

    private static void UpdateOrderWithNewValues(ref Order current, OrderDto incoming)
    {
        var shippingAddress = incoming.ShippingAddress.ToAddress();
        var billingAddress = incoming.BillingAddress.ToAddress();
        var payment = incoming.Payment.ToPayment();

        current.Update(
            OrderName.Of(incoming.OrderName),
            shippingAddress,
            billingAddress,
            payment,
            incoming.Status);
    }
}
