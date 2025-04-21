using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EShopping.Catalog.Features.Products.CreateProduct;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/products", Handle)
            .WithName("CreateProduct")
            .WithTags("Products")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Creates a Product");
    }

    private async Task<IResult> Handle(CreateProductRequest request, ISender sender)
    {
        var command = request.Adapt<CreateProductCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreateProductResponse>();
        return Results.Created($"/products/{result.Id}", response);
    }
}
