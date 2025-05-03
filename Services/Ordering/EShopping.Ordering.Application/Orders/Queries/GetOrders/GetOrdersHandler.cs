using EShopping.Shared.BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace EShopping.Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await context.Orders.LongCountAsync(cancellationToken);

            var orders = await context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Skip(request.Page.PageIndex * request.Page.PageSize)
                .Take(request.Page.PageSize)
                .ToListAsync(cancellationToken);

            var paginated = new PaginatedResult<OrderDto>(request.Page.PageIndex, request.Page.PageSize, totalCount, orders.ToOrderDtoList());

            return new GetOrdersResult(paginated);
        }
    }
}
