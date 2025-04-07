using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByToken :IRequest<string>
    {
        public string? Email { get; set; }

        public string? Token { get; set; }

        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
