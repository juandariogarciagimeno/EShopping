
using EShopping.Basket.Features.Basket.GetBasket;
using System.Reflection.Metadata;

namespace EShopping.Basket.Features.Basket.StoreBasket;

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/basket/", Handle)
            .WithName("StoreBasket")
            .WithTags("Basket")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Stores or updates a basket")
            .WithDescription("Stores or updates a basket");
    }

    public static async Task<IResult> Handle(StoreBasketRequest request, ISender sender)
    {
        var command = request.Adapt<StoreBasketCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<StoreBasketResponse>();
        return Results.Ok(response);
    }
}
