using EcommerceNet8.Core.Aplication.Contracts.Identity;
using EcommerceNet8.Core.Aplication.Exceptions;
using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;

        private readonly RoleManager<IdentityRole> _roleManger;

        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManger, IAuthService authService)
        {
            _userManager = userManager;
            _roleManger = roleManger;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
          
            var usuario= await _userManager.FindByNameAsync(_authService.GetSessionUser());


            if (usuario is null)
            {
                throw new BadRequestExcpetion("El usuario no existe");
            }


            usuario.Name = request.Name;
            usuario.LastName = request.LastName;
            usuario.PhoneNumber = request.PhoneNumber;
            usuario.AvatarUrl = request.PhotoUrl ?? usuario.AvatarUrl;


            var result = await _userManager.UpdateAsync(usuario);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo actualizar");
            }

          //  var userById = await _userManager.FindByEmailAsync(request.Email!);
            var roles = await _userManager.GetRolesAsync(usuario);



            return new AuthResponse
            {
                Id = usuario.Id, // Usá usuario en lugar de userById
                Name = usuario.Name,
                LastName = usuario.LastName,
                Phone = usuario.PhoneNumber,
                Email = usuario.Email,
                Username = usuario.UserName,
                Avatar = usuario.AvatarUrl,
                Token = _authService.CreateToken(usuario, roles),
                Roles = roles
            };
        }
    }
}
