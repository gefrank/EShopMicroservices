namespace Basket.API.Data
{
    // Inject IBasketRepository into CachedBasketRepository and implement the IBasketRepository interface
    // This implements both the Proxy and Decorator patterns
    // Proxy: The CachedBasketRepository class acts as a proxy for the IBasketRepository interface
    // Decorator: The CachedBasketRepository extends the class by adding caching functionality to the IBasketRepository interface
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            // Check if object is in the distributed cache
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            // Proxy forwards the call to the repository 
            var basket = await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
                  
            return basket; 
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }
    }
}
