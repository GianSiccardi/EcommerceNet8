using EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductList;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceNet8.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController :ControllerBase


       
    {

        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("list",Name ="GetProductList")]
        [ProducesResponseType(typeof(IReadOnlyList<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<ProductVm>>> GetProductList()
        {
            var query = new GetProductListQuery();
           var products= await _mediator.Send(query);

            return Ok(products);
        }
    }
}
