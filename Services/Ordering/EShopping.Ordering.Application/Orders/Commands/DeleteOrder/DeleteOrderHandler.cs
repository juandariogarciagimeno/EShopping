
namespace EShopping.Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);
        var order = await context.Orders.FindAsync([orderId], cancellationToken) ?? throw NotFoundException.Order(orderId);

        context.Orders.Remove(order);
        var res = await context.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(res > 0);
    }
}
