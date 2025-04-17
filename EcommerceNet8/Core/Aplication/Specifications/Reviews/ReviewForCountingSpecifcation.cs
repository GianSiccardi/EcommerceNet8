using EcommerceNet8.Core.Domain;
using System.Linq.Expressions;

namespace EcommerceNet8.Core.Aplication.Specifications.Reviews
{
    public class ReviewForCountingSpecifcation : BaseSpecification<Review>
    {
        public ReviewForCountingSpecifcation(ReviewSpecificationParams reviewsParams) : base(
            x => (!reviewsParams.ProductId.HasValue || x.ProductId == reviewsParams.ProductId))
        {
        }

       
    }
}
