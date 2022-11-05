using core.Models;

namespace core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetById(long id);
    Task<IReadOnlyList<Product>> GetAllAsync();
}