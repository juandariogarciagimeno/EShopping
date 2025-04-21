
using EShopping.Catalog.Features.Products.UpdateProduct;

namespace EShopping.Catalog.Features.Products.DeleteProduct
{
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app
                .MapDelete("/products/{id}", Handle)
                .WithName("DeleteProduct")
                .WithTags("Products")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Deletes a Product");
        }

        private async Task<IResult> Handle(Guid id, ISender sender)
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            var response = result.Adapt<DeleteProductResponse>();
            return Results.Ok(response);
        }
    }
}
