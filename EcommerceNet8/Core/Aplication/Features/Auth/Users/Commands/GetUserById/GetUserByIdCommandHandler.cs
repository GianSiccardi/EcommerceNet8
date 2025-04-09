using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.GetUserById
{
    public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, AuthResponse>
    {


        private readonly UserManager<Usuario> _userManager;

        public GetUserByIdCommandHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var userr = await _userManager.FindByIdAsync(request.UserId!);

            if(userr is null)
            {
                throw new Exception("El usuario no existe");
            }

            return new AuthResponse
            {
                Id = userr.Id,
                Name = userr.Name,
                LastName = userr.LastName,
                Phone = userr.PhoneNumber,
                Email = userr.Email,
                Username = userr.UserName,
                Avatar = userr.AvatarUrl,
                Roles = await _userManager.GetRolesAsync(userr)

            };
        }
    }
}
