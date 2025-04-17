using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Reviews.Queries.Vms;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewVm>
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReviewVm> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewEntity = new Review
            {
                Comment = request.Comment,
                Rating = request.Raiting,
                Name = request.Name,
                ProductId = request.ProductId

            };

            _unitOfWork.Repository<Review>().AddEntity(reviewEntity);
            var resultado = await _unitOfWork.Complete();

            if (resultado <= 0)
            {
                throw new Exception("No se puedo guardar el comentario");
            }

            return _mapper.Map<ReviewVm>(reviewEntity);

        }
    }
}
