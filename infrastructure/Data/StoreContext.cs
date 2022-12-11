using System.Reflection;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductBrand> ProductBrands { get; set; }
    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var props = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                foreach (var prop in props)
                {
                    modelBuilder.Entity(entityType.Name).Property(prop.Name).HasConversion<double>();
                }
            }
        }
    }
}