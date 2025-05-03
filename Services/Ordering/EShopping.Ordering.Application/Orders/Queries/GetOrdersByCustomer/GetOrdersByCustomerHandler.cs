using Microsoft.EntityFrameworkCore;

namespace EShopping.Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var customerId = CustomerId.Of(query.CustomerId);
        var orders = await context.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(x => x.CustomerId == customerId.Value)
            .ToListAsync(cancellationToken);

        var ordersDto = orders.ToOrderDtoList();

        return new GetOrdersByCustomerResult(ordersDto);
    }
}
