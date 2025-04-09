using AutoMapper;
using EcommerceNet8.Core.Aplication.Contracts.Identity;
using EcommerceNet8.Core.Aplication.Features.Address.Vms;
using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.GetUserByToken
{
    public class GetByTokenCommandHandler : IRequestHandler<GetByTokenCommand, AuthResponse>
    {

        private readonly UserManager<Usuario> userManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetByTokenCommandHandler(UserManager<Usuario> userManager, IAuthService authService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            _authService = authService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(GetByTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(_authService.GetSessionUser());

            if (user is null)
            {
                throw new Exception("El usuario no existe");
            }

            if (user.IsActive.Equals("0"))
            {
                throw new Exception("El usuario esta bloqueado");
            }

            var address = await _unitOfWork.Repository<Adress>().GetEntityAsync(x => x.Username == user.UserName);

            var roles = await userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                Roles = roles,
                ShippingAddress = _mapper.Map<AddresVm>(address),
                Token=_authService.CreateToken(user,roles)

            };

        }
    }
}
