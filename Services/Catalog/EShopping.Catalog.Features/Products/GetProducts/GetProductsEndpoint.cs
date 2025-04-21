namespace EShopping.Catalog.Features.Products.GetProducts
{
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app
                .MapGet("/products", Handle)
                .WithName("GetProducts")
                .WithTags("Products")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Lists Products");
        }

        public async Task<IResult> Handle([AsParameters] GetProductsRequest request, ISender sender)
        {
            var command = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(command);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        }   
    }
}
