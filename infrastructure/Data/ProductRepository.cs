using core.Interfaces;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetById(long id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        return (await _context.Products.ToListAsync()) as IReadOnlyList<Product>;
    }
}