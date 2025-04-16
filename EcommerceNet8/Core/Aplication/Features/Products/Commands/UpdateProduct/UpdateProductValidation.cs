using EcommerceNet8.Core.Aplication.Features.Products.Commands.CreateProduct;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using FluentValidation;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidation()
        {

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Nombre no puede estar en blanco")
                .MaximumLength(50).WithMessage("{Nombre} no puede exceder los 50 caracteres");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("El Description no puede ser nula");

            RuleFor(p => p.Stock)
            .NotEmpty().WithMessage("El Stock no puede ser nula");


            RuleFor(p => p.Price)
.NotEmpty().WithMessage("El Price no puede ser nula");

        }
    }
}
