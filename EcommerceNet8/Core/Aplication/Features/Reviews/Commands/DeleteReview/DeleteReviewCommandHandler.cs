using EcommerceNet8.Core.Aplication.Exceptions;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceNet8.Core.Aplication.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand , Unit>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task <Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewToDelete = await _unitOfWork.Repository<Review>().GetByIdAsync(request.ReviewId);

            if(reviewToDelete is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            await _unitOfWork.Repository<Review>().DeleteAsync(reviewToDelete);
            var resultado = await _unitOfWork.Complete();
            if (resultado <= 0)
            {
                throw new Exception("No se pudo eliminar el comentario.");
            }

            return Unit.Value;

        }
         
   
    }
}
