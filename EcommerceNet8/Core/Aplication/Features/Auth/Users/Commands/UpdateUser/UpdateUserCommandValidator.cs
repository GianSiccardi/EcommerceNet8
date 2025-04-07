using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {

        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre no puede ser nulo");


            RuleFor(p => p.Name)
           .NotEmpty().WithMessage("El apellido no puede ser nulo");
        }

    }
}
