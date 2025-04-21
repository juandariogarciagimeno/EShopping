using EShopping.Catalog.Data.Models;

namespace EShopping.Catalog.Data.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product?> Add(Product product, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Product>> ListAll(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        public Task<Product?> GetById(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Product>?> GetByCategory(string category, CancellationToken cancellationToken = default);
        public Task<Product?> Update(Product product, CancellationToken cancellationToken = default);
        public Task<bool> Delete(Product product, CancellationToken cancellationToken = default);
    }
}
