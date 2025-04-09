using EcommerceNet8.Core.Aplication.Features.Categories.Query;
using EcommerceNet8.Core.Aplication.Features.Categories.VmS;
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
    public class CategoryController : ControllerBase
    {

        private IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize]
        [HttpGet(Name = "GetCategory")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CountryVm>>> GetCategory()
        {
            var query = new GetCategoryListQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
