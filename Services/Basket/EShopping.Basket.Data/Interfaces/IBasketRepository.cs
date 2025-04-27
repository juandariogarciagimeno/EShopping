using EShopping.Basket.Data.Models;

namespace EShopping.Basket.Data.Interfaces;

public interface IBasketRepository
{
    Task<ShoppingCart> AddOrUpdateBasket(ShoppingCart basket, CancellationToken cancellationToken = default);
    Task<ShoppingCart?> GetBasket(string userName, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}