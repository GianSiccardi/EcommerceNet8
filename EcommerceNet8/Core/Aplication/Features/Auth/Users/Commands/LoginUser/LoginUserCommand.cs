using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AuthResponse>

    {
        public string? Email { get; set; }

        public string Password{ get; set; }

    }
}
