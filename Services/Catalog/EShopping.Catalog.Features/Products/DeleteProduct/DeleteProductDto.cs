namespace EShopping.Catalog.Features.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public record DeleteProductResponse(bool IsSuccess);
