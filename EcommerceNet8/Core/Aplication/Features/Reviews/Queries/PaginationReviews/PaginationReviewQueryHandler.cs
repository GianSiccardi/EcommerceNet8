using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Reviews.Queries.Vms;
using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Aplication.Specifications.Reviews;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Reviews.Queries.PaginationReviews
{
    public class PaginationReviewQueryHandler : IRequestHandler<PaginationReviewsQuery, PaginationVm<ReviewVm>>
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public PaginationReviewQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<ReviewVm>> Handle(PaginationReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviewSpecificationParams = new ReviewSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort
            };

            var spec = new ReviewSpecification(reviewSpecificationParams);
            var reviews = await _unitOfWork.Repository<Review>().GetAllWithSpec(spec);


            var specCount = new ReviewForCountingSpecifcation(reviewSpecificationParams);
            var totalReviews = await _unitOfWork.Repository<Review>().CountAsync(spec);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalReviews) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);


            var data = _mapper.Map<IReadOnlyList<Review>, IReadOnlyList<ReviewVm>>(reviews);

            var productByPage = reviews.Count();

            var pagination = new PaginationVm<ReviewVm>
            {
                Count = totalReviews,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productByPage
            };

            return pagination;

        }
    }
}
