using EcommerceNet8.Core.Aplication.Contracts.Infrastructure;
using EcommerceNet8.Core.Aplication.Models.Email;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceNet8.Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public TestController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SendEmail()
        {
            var message = new EmailMessage
            {
                To = "gianfranco@serviciosoptima.com",
                Body = "prueba mail",
                Subjet = "cambia password"
            };

            var result =await _emailService.SendEmailAsync(message, "token temporal");

            return result ? Ok() : BadRequest();

            

        }
    }
}
