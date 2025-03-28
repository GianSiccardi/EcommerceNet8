using EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductList;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceNet8.Api.Controllers
{

    [ApiController]
    [Route("ap/v1/[controller]")]
    public class ProductController :ControllerBase


       
    {

        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("list",Name ="GetProductList")]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductList()
        {
            var query = new GetProductListQuery();
           var products= await _mediator.Send(query);

            return Ok(products);
        }
    }
}
