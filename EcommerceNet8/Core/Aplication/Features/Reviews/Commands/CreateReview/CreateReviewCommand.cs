using EcommerceNet8.Core.Aplication.Features.Reviews.Queries.Vms;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<ReviewVm>
    {
        public int ProductId { get; set; }

        public  string? Name { get; set; }

        public int Raiting { get; set; }

        public string? Comment { get; set; }

    }
}
