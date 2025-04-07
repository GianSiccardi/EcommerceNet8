using FluentValidation;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
    

        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El mail no puede ser nulo");


            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("El password no puede ser nulo");

        }

    }
}
