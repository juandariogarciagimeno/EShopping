
using EShopping.Catalog.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace EShopping.Catalog.Features.Products.GetProductsByCategory
{
    public class GetProductsByCategoryHandler(IProductRepository repo) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await repo.GetByCategory(request.Category, cancellationToken);
            if (result is null)
            {
                return new GetProductsByCategoryResult([]);
            }

            var response = new GetProductsByCategoryResult(result);
            return response;
        }
    }
}
