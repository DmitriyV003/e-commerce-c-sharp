using core.Models;

namespace core.Specifications;

public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecification(ProductsSpecificationParams productParams)
        : base(x => 
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
    {
        
    }
}