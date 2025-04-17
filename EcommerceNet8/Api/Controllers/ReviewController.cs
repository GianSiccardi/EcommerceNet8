using CloudinaryDotNet.Actions;
using EcommerceNet8.Core.Aplication.Features.Reviews.Commands.CreateReview;
using EcommerceNet8.Core.Aplication.Features.Reviews.Commands.DeleteReview;
using EcommerceNet8.Core.Aplication.Features.Reviews.Queries.PaginationReviews;
using EcommerceNet8.Core.Aplication.Features.Reviews.Queries.Vms;
using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Role = EcommerceNet8.Core.Aplication.Models.Authorization.Role;
namespace EcommerceNet8.Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {

        private IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost(Name ="CreateReview")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReviewVm>> CreateReview([FromBody]CreateReviewCommand request)
        {
            return await _mediator.Send(request);
        }


        [Authorize(Roles =Role.ADMIN)]
        [HttpDelete("{id}",Name = "DeleteReview")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> DeleteReview(int id)
        {
            var request = new DeleteReviewCommand(id);
            return await _mediator.Send(request);


        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("paginationReviews", Name = "GetReview")]
        [ProducesResponseType(typeof(PaginationVm<ReviewVm>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> PaginationReview([FromQuery]PaginationReviewsQuery request)
        {
            var paginationReview = await _mediator.Send(request);
            return Ok(paginationReview);


        }
    }
}
