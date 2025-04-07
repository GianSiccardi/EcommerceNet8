using FluentValidation;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
    {
        public UpdateAdminUserCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("El nombre ni puede estar vacio");

            RuleFor(x => x.LastName)
              .NotEmpty().WithMessage("El nombre ni puede estar vacio");


            RuleFor(x => x.PhoneNumber)
              .NotEmpty().WithMessage("El nombre ni puede estar vacio");

            ;
        }
    }
}
