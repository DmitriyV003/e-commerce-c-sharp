using core.Models;
using core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class SpecificationEvaluator<TModel> where TModel : Base
{
    public static IQueryable<TModel> GetQuery(IQueryable<TModel> inputQuery, ISpecification<TModel> specification)
    {
        var query = inputQuery;

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }
        
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        
        if (specification.OrderByDesc != null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        if (specification.IsPaginatingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}