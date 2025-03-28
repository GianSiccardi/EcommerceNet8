using System.Linq.Expressions;

namespace EcommerceNet8.Core.Aplication.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get;}

        List<Expression<Func<T ,Object>>>? Includes { get; }

        Expression<Func<T,object>>? OrderBy { get; }

        Expression<Func<T,object>>? OrderByDescendig { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnable{ get; }

    }
}
