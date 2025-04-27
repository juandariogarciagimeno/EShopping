using EShopping.Basket.Data.Interfaces;
using EShopping.Basket.Data.Models;
using EShopping.Shared.BuildingBlocks.Exceptions;
using Marten;

namespace EShopping.Basket.Data.Repository;

public class BasketRepository(IDocumentSession Session) : IBasketRepository
{
    public async Task<ShoppingCart> AddOrUpdateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        Session.Store(basket);
        await Session.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        Session.Delete<ShoppingCart>(userName);
        await Session.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShoppingCart?> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await Session.LoadAsync<ShoppingCart>(userName, cancellationToken);

        return basket ?? throw NotFoundException.Basket(userName);
    }
}
