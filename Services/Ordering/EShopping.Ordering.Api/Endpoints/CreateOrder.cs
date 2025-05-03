using EShopping.Ordering.Application.Orders.Commands.CreateOrder;

namespace EShopping.Ordering.Api.Endpoints;

public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/orders", Handle)
            .WithName("CreateOrder")
            .WithTags("Orders")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
    }

    private static async Task<IResult> Handle(CreateOrderRequest request, ISender sender)
    {
        var command = request.Adapt<CreateOrderCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreateOrderResponse>();

        return Results.Created($"/orders/{response.Id}", response);
    }
}
