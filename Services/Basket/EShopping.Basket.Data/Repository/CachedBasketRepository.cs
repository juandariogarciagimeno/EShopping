using EShopping.Basket.Data.Interfaces;
using EShopping.Basket.Data.Models;
using EShopping.Shared.Utils;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace EShopping.Basket.Data.Repository
{
    public class CachedBasketRepository(IBasketRepository repo, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> AddOrUpdateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            var added = await repo.AddOrUpdateBasket(basket, cancellationToken);
            if (added is not null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };
                await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), options, cancellationToken);

                return added;
            }

            throw new Exception("Error storing basket");
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            bool result = await repo.DeleteBasket(userName, cancellationToken);

            if (result)
            {
                await cache.RemoveAsync(userName, cancellationToken);
            }

            return result;
        }

        public async Task<ShoppingCart?> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cached = await cache.GetStringAsync(userName, cancellationToken);
            if (cached is not null && cached.TryDeserialize<ShoppingCart>(out var cachedBasket))
            {
                return cachedBasket;
            }

            var val = await repo.GetBasket(userName, cancellationToken);
            if (val is not null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };
                await cache.SetStringAsync(userName, JsonSerializer.Serialize(val), options, cancellationToken);
            }

            return val;
        }
    }
}
