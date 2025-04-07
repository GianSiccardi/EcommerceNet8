using EcommerceNet8.Core.Aplication.Exceptions;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenHandler : IRequestHandler<ResetPasswordByToken, string>
    {


        private readonly UserManager<Usuario> _userManager;

        public ResetPasswordByTokenHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(ResetPasswordByToken request, CancellationToken cancellationToken)
        {
           if(!string.Equals(request.Password , request.ConfirmPassword) )
            {
                throw new BadRequestExcpetion("Las claves no coinciden");
            }
            var updateUsuario = await _userManager.FindByEmailAsync(request.Email);

            if(updateUsuario is null)
            {
                throw new BadRequestExcpetion("El email no esta registrado");
            }

            var token = Convert.FromBase64String(request.Token!);

            var tokenResult = Encoding.UTF8.GetString(token);

           var result= await _userManager.ResetPasswordAsync(updateUsuario, tokenResult ,request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo resetear el password");
            }

            return $"Se actualizo exitosamente tu clave";
        }
    }
}
