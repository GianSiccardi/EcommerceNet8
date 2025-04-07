using EcommerceNet8.Core.Aplication.Contracts.Infrastructure;
using EcommerceNet8.Core.Aplication.Models.Email;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.SendPassword
{
    public class SendPasswordCommandHandler : IRequestHandler<SendPasswordCommand, string>
    {


        private readonly IEmailService _serviceEmail;
        private readonly UserManager<Usuario> _userManager;

        public SendPasswordCommandHandler(IEmailService serviceEmail, UserManager<Usuario> userManager)
        {
            _serviceEmail = serviceEmail;
            _userManager = userManager;
        }

        public async Task<string> Handle(SendPasswordCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente= await _userManager.FindByEmailAsync(request.Email!);

            if(usuarioExistente is null)
            {
                throw new BadHttpRequestException("El usuario no existe");
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(usuarioExistente);

            var plainTextBytes = Encoding.UTF8.GetBytes( token);

            token = Convert.ToBase64String(plainTextBytes);

            var emailMessage = new EmailMessage
            {
                To = request.Email,
                Body = "Resetear el password , dale click aqui:",
                Subjet = "Cambiar el password"
            };

           var result= await _serviceEmail.SendEmailAsync(emailMessage, token);


            if (!result)
            {
                throw new Exception("No se pudo enviar el email");
            }

            return $"Se envio el email a la cuenta {request.Email}";
        }
    }
}
