using EShopping.Catalog.Data.Interfaces;
using EShopping.Catalog.Data.Models;
using Marten;
using Marten.Pagination;

namespace EShopping.Catalog.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private IDocumentSession _session;

    public ProductRepository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Product?> Add(Product product, CancellationToken cancellationToken = default)
    {
        try
        {
            _session.Store(product);
            await _session.SaveChangesAsync(cancellationToken);
            return product;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> Delete(Product product, CancellationToken cancellationToken = default)
    {
        try
        {
            _session.Delete(product);
            await _session.SaveChangesAsync(cancellationToken);
            return true;
        } 
        catch 
        {
            return false;
        }
    }

    public async Task<IEnumerable<Product>?> GetByCategory(string category, CancellationToken cancellationToken = default)
    {
        return await _session.Query<Product>()
            .Where(x => x.Category.Contains(category))
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _session.LoadAsync<Product>(id, cancellationToken);
    }

    public async Task<IEnumerable<Product>> ListAll(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _session.Query<Product>().ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<Product?> Update(Product product, CancellationToken cancellationToken = default)
    {
        try
        {
            _session.Update<Product>(product);
            await _session.SaveChangesAsync(cancellationToken);
            return product;
        }
        catch
        {
            return null;
        }
    }
}
