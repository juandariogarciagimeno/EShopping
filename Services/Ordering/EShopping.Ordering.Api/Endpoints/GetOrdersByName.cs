using EShopping.Ordering.Application.Orders.Queries.GetOrdersByName;

namespace EShopping.Ordering.Api.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/orders/{orderName}", Handle)
            .WithName("GetOrdersByName")
            .WithTags("Orders")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name");
    }

    private static async Task<IResult> Handle(string orderName, ISender sender)
    {
        var query = new GetOrdersByNameQuery(orderName);
        var result = await sender.Send(query);
        var response = result.Adapt<GetOrdersByNameResponse>();

        return Results.Ok(response);
    }
}
