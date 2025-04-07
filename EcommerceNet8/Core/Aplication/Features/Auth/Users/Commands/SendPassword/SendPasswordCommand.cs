using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.SendPassword
{
    public class SendPasswordCommand : IRequest<string>
    {
        public string? Email { get; set; }

    }
}
