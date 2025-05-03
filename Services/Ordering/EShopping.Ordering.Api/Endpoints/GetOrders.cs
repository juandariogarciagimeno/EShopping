using EShopping.Ordering.Application.Orders.Queries.GetOrders;
using EShopping.Shared.BuildingBlocks.Pagination;

namespace EShopping.Ordering.Api.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/orders", Handle)
            .WithName("GetOrders")
            .WithTags("Orders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
    }

    private static async Task<IResult> Handle([AsParameters] PaginationRequest request, ISender sender)
    {
        var query = new GetOrdersQuery(request);
        var result = await sender.Send(query);
        var response = result.Adapt<GetOrdersResponse>();

        return Results.Ok(response);
    }
}
