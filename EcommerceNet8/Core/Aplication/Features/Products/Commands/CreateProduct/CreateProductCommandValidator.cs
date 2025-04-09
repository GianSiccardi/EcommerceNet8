using FluentValidation;

namespace EcommerceNet8.Core.Aplication.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {

            RuleFor(p => p.Name)
                    .NotEmpty().WithMessage("El nombre no puede estar en blanco");

            RuleFor(p => p.Description)
                 .NotEmpty().WithMessage("La descripcion no puede estar en blanco");

            RuleFor(p=>p.Price)
                     .NotEmpty().WithMessage("El precio no puede ser nulo ");
        }
    }
}
