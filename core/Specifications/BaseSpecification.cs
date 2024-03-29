﻿using System.Linq.Expressions;

namespace core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{

    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDesc { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPaginatingEnabled { get; private set; }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public BaseSpecification()
    {
    }

    protected void AddInclude(Expression<Func<T, object>> include)
    {
        Includes.Add(include);
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    
    protected void AddOrderByDesc(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDesc = orderByExpression;
    }

    protected void Paginate(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPaginatingEnabled = true;
    }
}