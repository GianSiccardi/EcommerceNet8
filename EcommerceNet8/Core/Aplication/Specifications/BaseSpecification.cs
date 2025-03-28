using System.Linq.Expressions;

namespace EcommerceNet8.Core.Aplication.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>

    {
        public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<T,bool>>criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>>? Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T,Object>>>();

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescendig { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnable { get; private set; }

        protected void AddOrderBY(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderBYDescending(Expression<Func<T, object>> orderByExpression)
        {
            OrderByDescendig = orderByExpression;
        }

        protected void ApplyPaging(int skip ,int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnable = true;

        }

        protected void AddInclude(Expression<Func<T , object>> includesExpression)
        {
            Includes.Add(includesExpression);
        }

    }
}
