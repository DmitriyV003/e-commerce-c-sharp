using System.Linq.Expressions;
using core.Models;

namespace core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductsSpecificationParams productParams) 
        : base(x => 
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
    {
        if (string.IsNullOrEmpty(productParams.Sort)) return;
        switch (productParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
        
        Paginate(productParams.PerPage * (productParams.Page - 1), productParams.PerPage);
    }

    public ProductsWithTypesAndBrandsSpecification(Expression<Func<Product, bool>> criteria, string? sort = null) : base(criteria)
    {
    }

    public ProductsWithTypesAndBrandsSpecification WithBrands()
    {
        AddInclude(x => x.ProductBrand);
        return this;
    }
    
    public ProductsWithTypesAndBrandsSpecification WithTypes()
    {
        AddInclude(x => x.ProductType);
        return this;
    }
}