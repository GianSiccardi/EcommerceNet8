using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.GetUserByToken
{
    public class GetByTokenCommand : IRequest<AuthResponse>
    {
    }
}
