using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<AuthResponse>

    {
        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

       

        public string? PhoneNumber { get; set; }

        public  IFormFile? Photo { get; set; }

        public string? PhotoUrl { get; set; }

        public string? FotoId { get; set; }



    }
}
