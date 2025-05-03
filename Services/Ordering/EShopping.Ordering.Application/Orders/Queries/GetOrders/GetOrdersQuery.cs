using EShopping.Shared.BuildingBlocks.Pagination;

namespace EShopping.Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest Page) : IQuery<GetOrdersResult>;
public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
