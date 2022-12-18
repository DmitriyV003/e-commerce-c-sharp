using core.Models;
using core.Specifications;

namespace core.Interfaces;

public interface IGenericRepository<T> where T : Base
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetModelWithSpecification(ISpecification<T> specification);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> spec);
}