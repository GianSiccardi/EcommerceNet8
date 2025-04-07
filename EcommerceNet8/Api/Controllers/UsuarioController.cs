using EcommerceNet8.Core.Aplication.Contracts.Infrastructure;
using EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.LoginUser;
using EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.RegisterUser;
using EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.ResetPassword;
using EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.ResetPasswordByToken;
using EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.SendPassword;
using EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateAdminUser;
using EcommerceNet8.Core.Aplication.Features.Auth.Users.Commands.UpdateUser;
using EcommerceNet8.Core.Aplication.Features.Auth.Vms;
using EcommerceNet8.Core.Aplication.Models.Authorization;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Role = EcommerceNet8.Core.Aplication.Models.Authorization.Role; 
namespace EcommerceNet8.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private IMediator _mediator;
        private IManageImageService _manageImageService;

        public UsuarioController(IMediator mediator, IManageImageService manageImageService)
        {
            _mediator = mediator;
            _manageImageService = manageImageService;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPost("register", Name = "Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<AuthResponse>> Register([FromForm] RegisterUserCommand request)
        {
            if (request.Photo is not null)
            {
                var resultImage = await _manageImageService.UploadImage(new Core.Aplication.Models.ImageMangment.ImageData
                {
                    ImageStream = request.Photo!.OpenReadStream(),
                    Name = request.Name
                }

                    );

                request.PhotoId = resultImage.PublicId;
                request.PhotoUrl = resultImage.Url;
            }

            return await _mediator.Send(request);
        }


        [AllowAnonymous]
        [HttpPost("forgotpassword", Name = "ForgotPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] SendPasswordCommand request)
        {
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPost("resetpassword", Name = "ResetPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByToken request)
        {
            return await _mediator.Send(request);
        }


        [HttpPost("updatepassword", Name = "UpdatePassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> UpdatePassword([FromBody] ResetPasswordCommond request)
        {
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpPut("update", Name = "Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Update([FromForm] UpdateUserCommand  request)
        {

          
            if (request.Photo is not null){
                var resultImage = await _manageImageService.UploadImage(new Core.Aplication.Models.ImageMangment.ImageData
                {
                    ImageStream = request.Photo!.OpenReadStream(),
                    Name = request.Photo!.Name
                });


                request.FotoId = resultImage.PublicId;
                request.PhotoUrl = resultImage.Url;
            }

            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("updateAdminUser", Name = "UpdateAdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Usuario>> UpdateAdminUser([FromBody] UpdateAdminUserCommand request) {
            Console.WriteLine($"Rol esperado: {Core.Aplication.Models.Authorization.Role.ADMIN}");
            Console.WriteLine($"Usuario autenticado: {User.Identity?.IsAuthenticated}");
            Console.WriteLine($"Claims: {string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}"))}");

            return await _mediator.Send(request);
        }
    }
}
