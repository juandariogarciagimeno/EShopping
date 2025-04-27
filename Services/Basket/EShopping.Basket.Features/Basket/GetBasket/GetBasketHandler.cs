using EShopping.Basket.Data.Interfaces;

namespace EShopping.Basket.Features.Basket.GetBasket;

public class GetBasketHandler(IBasketRepository repo) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var result = await repo.GetBasket(request.UserName, cancellationToken);
        if (result is null)
        {
            return new GetBasketResult(new ShoppingCart());
        }
        var response = new GetBasketResult(result);
        return response;
    }
}