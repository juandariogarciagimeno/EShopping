

namespace EShopping.Basket.Features.Basket.DeleteBasket;

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapDelete("/basket/{userName}", Handle)
            .WithName("DelBasket")
            .WithTags("Basket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Deletes a basket")
            .WithDescription("Deletes a basket");
    }

    public static async Task<IResult> Handle(string userName, ISender sender)
    {
        var result = await sender.Send(new DeleteBasketCommand(userName));
        var response = result.Adapt<DeleteBasketResponse>();
        return Results.Ok(response);
    }
}
