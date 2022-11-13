using System.Linq.Expressions;
using core.Models;

namespace core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification() : base()
    {
    }

    public ProductsWithTypesAndBrandsSpecification(Expression<Func<Product, bool>> criteria) : base(criteria)
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