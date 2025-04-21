using EShopping.Catalog.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace EShopping.Catalog.Features.Products.GetProductById
{
    public class GetProductByIdHandler(IProductRepository repo) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await repo.GetById(request.Id, cancellationToken) ?? throw NotFoundException.Product(request.Id);
            var result = new GetProductByIdResult(res);

            return result;
        }
    }
}
