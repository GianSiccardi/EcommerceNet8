using FluentValidation;

namespace EcommerceNet8.Core.Aplication.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(P => P.Name)
                .NotNull().WithMessage("Nombre no permite valores nulos");


            RuleFor(p => p.Comment)
                .NotNull().WithMessage("El comentario no permite valores nulos");

            RuleFor(p => p.Raiting)
                .NotEmpty().WithMessage("Raiting no permite valores nulos");

        }
    }
}
