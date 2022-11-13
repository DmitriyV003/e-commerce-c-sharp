using core.Models;

namespace infrastructure.Data;

public class ProductRepository : GenericRepository<Product>
{
    public ProductRepository(StoreContext context) : base(context)
    {
    }
}