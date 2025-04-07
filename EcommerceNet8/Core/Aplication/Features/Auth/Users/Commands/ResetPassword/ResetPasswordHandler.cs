using EcommerceNet8.Core.Aplication.Contracts.Identity;
using EcommerceNet8.Core.Aplication.Exceptions;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommond , Unit> 
    {

        private readonly UserManager<Usuario> _userManager;

        private readonly IAuthService _authService;

        public ResetPasswordHandler(UserManager<Usuario> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<Unit> Handle(ResetPasswordCommond request, CancellationToken cancellationToken)
        {
            var updateUsuario = await _userManager.FindByEmailAsync(_authService.GetSessionUser());

            if(updateUsuario is null)
            {
                throw new BadRequestExcpetion("El usuario no existe");
            }

            var resulValidateOldPassword = _userManager.PasswordHasher.VerifyHashedPassword(updateUsuario, updateUsuario.PasswordHash, request.OldPassowrd);


            if(!(resulValidateOldPassword == PasswordVerificationResult.Success))
            {
                throw new BadRequestExcpetion("La clave actual ingreseda es erroneo");
            }

            var hashedNewPassword = _userManager.PasswordHasher.HashPassword(updateUsuario, request.NewPassowrd!);

            updateUsuario.PasswordHash = hashedNewPassword;

            var usuarioActuliazdo = await _userManager.UpdateAsync(updateUsuario);

            if (!usuarioActuliazdo.Succeeded)
            {
                throw new Exception("No se pudo resetear el password"); 

            }


            return Unit.Value;
        }

       
    }
}
