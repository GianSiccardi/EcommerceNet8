using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommand : IRequest<Usuario>
    {

        public string? Id { get; set; }


        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Role { get; set; }

    }
}
