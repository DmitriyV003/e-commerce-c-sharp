using core.Models;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Product> Products { get; set; }
}