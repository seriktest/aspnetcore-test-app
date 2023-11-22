using System.Text.Json;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repositories; 

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _cache;
    
    public BasketRepository(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }


    public async Task<ShoppingCart?> GetBasketAsync(string userName) {
        var basket = await _cache.GetStringAsync(userName).ConfigureAwait(false);
        if (string.IsNullOrEmpty(basket)) {
            return null;
        }
        return JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket) {
        await _cache.SetStringAsync(basket.UserName,
            JsonSerializer.Serialize(basket)).ConfigureAwait(false);
        return await GetBasketAsync(basket.UserName).ConfigureAwait(false);
    }

    public Task DeleteBasketAsync(string userName)
    {
        return _cache.RemoveAsync(userName);
    }
}