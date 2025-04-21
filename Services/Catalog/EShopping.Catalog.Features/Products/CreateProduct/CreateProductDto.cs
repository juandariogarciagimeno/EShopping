namespace EShopping.Catalog.Features.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, decimal Price, string ImageFile) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public record CreateProductRequest(string Name, List<string> Category, string Description, decimal Price, string ImageFile);
public record CreateProductResponse(Guid Id);