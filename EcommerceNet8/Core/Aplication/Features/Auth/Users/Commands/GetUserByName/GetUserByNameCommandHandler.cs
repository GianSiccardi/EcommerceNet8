using EcommerceNet8.Core.Aplication.Features.Address.Vms;
using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using EcommerceNet8.Core.Domain;
using FluentEmail.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.GetUserByName
{
    public class GetUserByNameCommandHandler : IRequestHandler<GetUserByUserNameQuery, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;

        public GetUserByNameCommandHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName!);

            if (user is null)
            {
                throw new Exception("El usuario no existe");
            }

            return new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                Roles = await _userManager.GetRolesAsync(user!)


            };
        }
    }
}
