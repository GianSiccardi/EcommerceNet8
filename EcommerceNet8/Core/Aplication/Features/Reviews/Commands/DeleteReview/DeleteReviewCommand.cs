using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommand : IRequest<Unit>
    {

        public int ReviewId { get; set; }

        public DeleteReviewCommand(int reviewId)
        {
            ReviewId = reviewId==0 ? throw new ArgumentNullException(nameof(reviewId)) : reviewId;
        }
    }
}
