using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminStatusUserCommand :IRequest<Usuario>
    {

        public string? Id { get; set; }

    }
}
