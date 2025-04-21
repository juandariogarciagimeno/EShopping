
using EShopping.Catalog.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace EShopping.Catalog.Features.Products.GetProducts
{
    public class GetProductsHandler(IProductRepository repo) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {

        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var res = await repo.ListAll(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);
            var response = new GetProductsResult(res);

            return response;
        }
    }
}
