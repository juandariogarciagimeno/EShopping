using EShopping.Catalog.Features.Products.GetProducts;

namespace EShopping.Catalog.Features.Products.GetProductsByCategory
{
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app
                .MapGet("/products/category/{category}", Handle)
                .WithName("GetProductsByCategory")
                .WithTags("Products")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .WithDescription("Gets a list of Product");
        }


        public async Task<IResult> Handle(string category, ISender sender)
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));
            var response = result.Adapt<GetProductsByCategoryResponse>();
            return Results.Ok(response);
        }
    }
}
