using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Product> Products { get; set; }
}