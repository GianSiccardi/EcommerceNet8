using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<AuthResponse>
    {

        public string? Name { get; set; }

        public string? UserName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public IFormFile? Photo { get; set; }

        public string? PhotoUrl { get; set; }

        public string? PhotoId { get; set; }

        public string? Password { get; set; }
    }
}
