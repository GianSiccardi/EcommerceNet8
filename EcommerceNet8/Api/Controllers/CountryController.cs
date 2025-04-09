using EcommerceNet8.Core.Aplication.Features.Countries.Querys.GetListCountries;
using EcommerceNet8.Core.Aplication.Features.Countries.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceNet8.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize]
        [HttpGet(Name ="GetCountries")]
        [ProducesResponseType(typeof(IReadOnlyList<CountryVm>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CountryVm>>>GetCountries()
        {
            var query = new  GetListQueryCountries();
            return Ok(await _mediator.Send(query));
        }
    }
}
