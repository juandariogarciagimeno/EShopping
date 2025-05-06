

using EShopping.Catalog.Features.Products.GetProducts;
using System.Reflection.Metadata;

namespace EShopping.Catalog.Features.Products.GetProductById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app
                .MapGet("/products/{id}", Handle)
                .WithName("GetProductById")
                .WithTags("Products")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Get a Product");
        }


        public async Task<IResult> Handle(Guid id, ISender sender)
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        }
    }
}
