using EShopping.Ordering.Application.Orders.Commands.UpdateOrder;

namespace EShopping.Ordering.Api.Endpoints;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPut("/orders", Handle)
            .WithName("UpdateOrder")
            .WithTags("Orders")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
    }

    private static async Task<IResult> Handle(UpdateOrderRequest request, ISender sender)
    {
        var command = request.Adapt<UpdateOrderCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<UpdateOrderResponse>();

        return Results.Ok(response);
    }
}
