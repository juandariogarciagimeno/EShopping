namespace EShopping.Web.Services;

public interface ICatalogService
{
    [Get("/products?pageNumber={pageNumber}&pageSize={pageSize}")]
    public Task<GetProductsResponse> GetProducts(int pageNumber = 1, int pageSize = 10);

    [Get("/products/{id}")] 
    public Task<GetProductByIdResponse> GetProductById(Guid id);

    [Get("/products/category/{category}")]
    public Task<GetProductsByCategoryResponse> GetProductsByCategory(string category);
}
