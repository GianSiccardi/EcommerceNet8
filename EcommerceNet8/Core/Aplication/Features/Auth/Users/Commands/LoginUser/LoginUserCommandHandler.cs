using AutoMapper;
using EcommerceNet8.Core.Aplication.Contracts.Identity;
using EcommerceNet8.Core.Aplication.Exceptions;
using EcommerceNet8.Core.Aplication.Features.Address.Vms;
using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using EcommerceNet8.Core.Domain;
using EcommerceNet8.Infraestructure.Repository;

using FluentEmail.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
    {

        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;
        private readonly UnitOfWork unitOfWork;

        public LoginUserCommandHandler(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager, IAuthService authService, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authService = authService;
            this.unitOfWork = unitOfWork;
        }
     
        

        public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);
            if(user is null)
            {
                throw new NotFoundException(nameof(Usuario), request.Email!);
            }


            if (user.IsActive is null)
            {
                throw new Exception("Usuario bloqueado , contacte administrador");
            }

           var result= await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);

            if (!result.Succeeded)
            {
                throw new Exception("Las credenciales del usuario son erroneas");
            }

            var roles = await _userManager.GetRolesAsync(user);

          var address=  await unitOfWork.Repository<Adress>().GetEntityAsync(x => x.Username == user.UserName);

            var authReponse = new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                ShippingAddress = _mapper.Map<AddresVm>(address),
                Token = _authService.CreateToken(user, roles),
                Roles = roles


            };

            return authReponse;
        }
    }
}
