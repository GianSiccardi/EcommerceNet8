using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.GetUserById
{
    public class GetUserByIdCommand : IRequest<AuthResponse>
    {

        public string UserId { get; set; }

        public GetUserByIdCommand(string userId)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        }
    }
}
