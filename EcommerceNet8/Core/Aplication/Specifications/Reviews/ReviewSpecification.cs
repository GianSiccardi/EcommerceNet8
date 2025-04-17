using EcommerceNet8.Core.Domain;
using System.Linq.Expressions;

namespace EcommerceNet8.Core.Aplication.Specifications.Reviews
{
    public class ReviewSpecification : BaseSpecification<Review>
    {
        public ReviewSpecification(ReviewSpecificationParams reviewParams) : base (
            x=>(!reviewParams.ProductId.HasValue || x.ProductId == reviewParams.ProductId))
        {

            ApplyPaging(reviewParams.PageSize * (reviewParams.PageIndex - 1), reviewParams.PageSize);

            if (!string.IsNullOrEmpty(reviewParams.Sort))
            {

                switch (reviewParams.Sort)
                {
                    case "createDateAsc":
                        AddOrderBY(p => p.CreatedDate!);
                        break;


                    case "createDateDesc":
                        AddOrderBYDescending(p => p.CreatedDate!);
                        break;


                    default:
                        AddOrderBY(p => p.CreatedDate!);
                        break;


                }

            }else
            {
                AddOrderBYDescending(P => P.CreatedDate!);
            }

        }

      
    }
}
