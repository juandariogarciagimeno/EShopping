using EShopping.Ordering.Application.Orders.Commands.DeleteOrder;

namespace EShopping.Ordering.Api.Endpoints;
public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapDelete("/orders/{Id}", Handle)
            .WithName("DeleteOrder")
            .WithTags("Orders")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
    }

    private static async Task<IResult> Handle(Guid Id, ISender sender)
    {
        var command = new DeleteOrderCommand(Id);
        var result = await sender.Send(command);
        var response = result.Adapt<DeleteOrderResponse>();

        return Results.Ok(response);
    }
}
