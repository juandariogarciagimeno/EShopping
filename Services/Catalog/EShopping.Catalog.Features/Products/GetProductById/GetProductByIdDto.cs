namespace EShopping.Catalog.Features.Products.GetProductById
{
    public record class GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record class GetProductByIdResult(Product Product);


    public record class GetProductByIdResponse(Product Product);
}
