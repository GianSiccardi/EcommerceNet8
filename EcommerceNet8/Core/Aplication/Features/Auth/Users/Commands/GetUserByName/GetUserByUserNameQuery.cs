using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.GetUserByName
{
    public class GetUserByUserNameQuery : IRequest<AuthResponse>
    {

        public string? UserName { get; set; }

        public GetUserByUserNameQuery(string? userName)
        {
            UserName = userName ?? throw new ArgumentException(nameof(UserName));
        }
    }
}
