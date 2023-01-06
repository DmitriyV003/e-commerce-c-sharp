using System.Text.Json;
using core.Models;
using core.Specifications;
using StackExchange.Redis;

namespace infrastructure.Data;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;
    
    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<Basket> GetAsync(string id)
    {
        var data = await _database.StringGetAsync(id);

        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
    }

    public async Task<Basket?> UpdateAsync(Basket basket)
    {
        var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

        if (!created) return null;

        return await GetAsync(basket.Id);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }
}