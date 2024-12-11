using NotifyMaster.Core.Specification;
using System.Linq.Expressions;

namespace NotifyMaster.Infrastructure.Specification;

public class BaseSpecification<T> : ISpecification<T>
{
    public bool AsNotTracking { get; set; } = true;
    public bool AsSplitQuery { get; set; } = true;

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();

    protected void AddInclude(Expression<Func<T, object>> include)
    {
        Includes.Add(include);
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
}