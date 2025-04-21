namespace EShopping.Catalog.Features.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);