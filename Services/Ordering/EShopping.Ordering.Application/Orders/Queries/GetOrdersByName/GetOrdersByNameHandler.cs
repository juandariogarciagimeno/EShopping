
using Microsoft.EntityFrameworkCore;

namespace EShopping.Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(query.Name))
            .OrderBy(x => x.OrderName.Value)
            .ToListAsync(cancellationToken);

        var orderDtos = orders.ToOrderDtoList();

        return new GetOrdersByNameResult(orderDtos);
    }
}

public class Order(OrderId id)
{
    public OrderId Id { get; set; } = id;
}

public readonly record struct OrderId(Guid id)
{
    public Guid Value { get; } = id;

    public static implicit operator Guid(OrderId oi) => oi.Value;
}

public class Test(IApplicationDbContext dbContext)
{
    public bool CheckIfOrderExists(Order order)
    {
        return dbContext.Orders.Find(order.Id) != null; //notice the .Value of the Id
    }
}