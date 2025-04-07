using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.ResetPassword
{
    public class ResetPasswordCommond :IRequest<Unit>
    {
        public string? NewPassowrd { get; set; }

        public string? OldPassowrd { get; set; }
    }
}
