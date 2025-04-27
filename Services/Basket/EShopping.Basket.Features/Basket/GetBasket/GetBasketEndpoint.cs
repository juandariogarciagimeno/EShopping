namespace EShopping.Basket.Features.Basket.GetBasket
{
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", Handle)
                .WithName("GetBasket")
                .WithTags("Basket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Basket By UserName")
                .WithDescription("Get Basket By UserName");
        }

        public static async Task<IResult> Handle(string userName, ISender sender)
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        }
    }
}
