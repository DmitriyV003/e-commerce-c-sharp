using System.Linq.Expressions;
using core.Models;

namespace core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(string sort) : base()
    {
        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
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
        }
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