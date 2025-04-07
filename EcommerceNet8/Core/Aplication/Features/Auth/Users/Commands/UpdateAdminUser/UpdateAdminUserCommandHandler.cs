using EcommerceNet8.Core.Aplication.Contracts.Identity;
using EcommerceNet8.Core.Aplication.Exceptions;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, Usuario>
    {

        private readonly UserManager<Usuario> userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IAuthService _authService;

        public UpdateAdminUserCommandHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            this.userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<Usuario> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var updateUsuario = await userManager.FindByIdAsync(request.Id!);

            if(updateUsuario is null)
            {
                throw new BadRequestExcpetion("El usuario no existe");
            }

            updateUsuario.Name = request.Name;
            updateUsuario.LastName = request.LastName;
            updateUsuario.PhoneNumber = request.PhoneNumber;

            var resultado = await userManager.UpdateAsync(updateUsuario);

            if (!resultado.Succeeded)
            {
                throw new Exception("No se pudo actualizar el usuario");
            }

            var role = await _roleManager.FindByNameAsync(request.Role!);

            if(role is null)
            {
                throw new Exception("El Rol asiganado no existe");
            }

            await userManager.AddToRoleAsync(updateUsuario, role.Name!);

            return updateUsuario;

        }
    }
}
