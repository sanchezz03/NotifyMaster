using System.Linq.Expressions;

namespace NotifyMaster.Core.Specification;

public interface ISpecification<T>
{
    bool AsNotTracking { get; set; }
    bool AsSplitQuery { get; set; }

    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
}