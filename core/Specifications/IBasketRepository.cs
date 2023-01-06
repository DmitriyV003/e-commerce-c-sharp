using core.Models;

namespace core.Specifications;

public interface IBasketRepository
{
    Task<Basket> GetAsync(string id);
    Task<Basket?> UpdateAsync(Basket basket);
    Task<bool> DeleteAsync(string id);
}