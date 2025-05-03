using EShopping.Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace EShopping.Ordering.Api.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/orders/customer/{customerId}", Handle)
            .WithName("GetOrdersByCustomer")
            .WithTags("Orders")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
    }

    private static async Task<IResult> Handle(Guid customerId, ISender sender)
    {
        var query = new GetOrdersByCustomerQuery(customerId);
        var result = await sender.Send(query);
        var response = result.Adapt<GetOrdersByCustomerResponse>();

        return Results.Ok(response);
    }
}
