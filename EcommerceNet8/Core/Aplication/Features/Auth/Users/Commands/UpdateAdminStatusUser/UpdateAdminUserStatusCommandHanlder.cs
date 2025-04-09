using EcommerceNet8.Core.Aplication.Exceptions;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminUserStatusCommandHanlder : IRequestHandler<UpdateAdminStatusUserCommand, Usuario>
    {


        private readonly UserManager<Usuario> _userManager;

        public UpdateAdminUserStatusCommandHanlder(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Usuario> Handle(UpdateAdminStatusUserCommand request, CancellationToken cancellationToken)
        {
            var updateUsuario = await _userManager.FindByIdAsync(request.Id!);

            if(updateUsuario is null)
            {
                throw new NotFoundException("No se encontro el usuario" , updateUsuario!);
            }

            updateUsuario.IsActive = updateUsuario.IsActive == "1" ? "0" : "1";

            var resultado = await _userManager.UpdateAsync(updateUsuario);


            if (!resultado.Succeeded)
            {

                throw new Exception("No se pudo actualizar el usuario");
            }

            return updateUsuario;

        }
    }
}
