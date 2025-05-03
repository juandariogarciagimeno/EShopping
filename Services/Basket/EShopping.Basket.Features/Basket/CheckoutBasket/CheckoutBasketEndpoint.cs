using EShopping.Basket.Features.Basket.DeleteBasket;

namespace EShopping.Basket.Features.Basket.CheckoutBasket
{
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app
                .MapPost("/basket/checkout", Handle)
                .WithName("CheckoutBasket")
                .WithTags("Basket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Checkout Basket")
                .WithDescription("Checkout Basket");
        }

        public static async Task<IResult> Handle(CheckoutBasketRequest request, ISender sender)
        {
            var command = request.Adapt<CheckoutBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CheckoutBasketResponse>();
            return Results.Ok(response);
        }
    }
}
