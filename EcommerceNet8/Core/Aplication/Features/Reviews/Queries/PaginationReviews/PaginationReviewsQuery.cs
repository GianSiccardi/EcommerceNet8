using EcommerceNet8.Core.Aplication.Features.Reviews.Queries.Vms;
using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Reviews.Queries.PaginationReviews
{
    public class PaginationReviewsQuery : PaginationBaseQuerys ,IRequest<PaginationVm<ReviewVm>>
    {
        public  int? ProductId { get; set; }

    }
}
