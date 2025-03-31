using EcommerceNet8.Core.Aplication.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EcommerceNet8.Infraestructure.Specifications
{
    public class SpecifitationEvaluator<T>where T :class
    {
        public static IQueryable<T>GetQuery(IQueryable<T>inputQuery, ISpecification<T> spec)
        {
            if(spec.Criteria !=null)
            {
                inputQuery = inputQuery.Where(spec.Criteria);
            }

            if(spec.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescendig != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderByDescendig);
            }


            if (spec.IsPagingEnable)
            {
                inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
            }

            inputQuery = spec.Includes!.Aggregate(inputQuery, (current, include) => current.Include(include)).AsSplitQuery().AsNoTracking();

            return inputQuery;
        }

    }
}
