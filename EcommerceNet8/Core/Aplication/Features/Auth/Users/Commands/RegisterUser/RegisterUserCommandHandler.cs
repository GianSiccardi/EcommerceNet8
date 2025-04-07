using EcommerceNet8.Core.Aplication.Contracts.Identity;
using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using EcommerceNet8.Core.Domain;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existenteUserByEmail = await _userManager.FindByEmailAsync(request.Email) is null ? false : true;

            if (existenteUserByEmail)
            {
                throw new BadHttpRequestException("El Email del usuario ya existe en la base de datos");
            }

            var existenteUserByUserName = await _userManager.FindByNameAsync(request.UserName) is null ? false : true;

            if (existenteUserByEmail)
            {
                throw new BadHttpRequestException("El nombre de usuario ya existe en la base de datos");
            }


            var usuario = new Usuario
            {
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UserName = request.UserName,
                AvatarUrl = request.PhotoUrl,
                IsActive="1"


            };

            var response = await _userManager.CreateAsync(usuario!, request.Password);

            if (response.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, Role.GenericUser);
                var roles = await _userManager.GetRolesAsync(usuario);

                return new AuthResponse
                {
                    Id = usuario.Id,
                    Name = usuario.Name,
                    LastName = usuario.LastName,
                    Phone = usuario.PhoneNumber,
                    Email = usuario.Email,
                    Username = usuario.UserName,
                    Avatar = usuario.AvatarUrl,
                    Token = _authService.CreateToken(usuario, roles)
                };
            }

            throw new Exception("No se pudo registrar el usuario");
        }
    }
}
