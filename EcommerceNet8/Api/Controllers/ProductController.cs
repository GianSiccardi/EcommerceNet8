using EcommerceNet8.Core.Aplication.Contracts.Infrastructure;
using EcommerceNet8.Core.Aplication.Features.Products.Commands.CreateProduct;
using EcommerceNet8.Core.Aplication.Features.Products.Commands.DeleteProduct;
using EcommerceNet8.Core.Aplication.Features.Products.Commands.UpdateProduct;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductById;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductList;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.PaginationProdcuts;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using EcommerceNet8.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Role = EcommerceNet8.Core.Aplication.Models.Authorization.Role;


namespace EcommerceNet8.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController :ControllerBase


       
    {

        private IMediator _mediator;

        private readonly IManageImageService _imanageImageService;

        public ProductController(IMediator mediator, IManageImageService imanageImageService)
        {
            _mediator = mediator;
            _imanageImageService = imanageImageService;
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

        [AllowAnonymous]
        [HttpGet("pagination", Name = "PaginatioProduct")]
        [ProducesResponseType(typeof(PaginationVm<ProductVm>) ,  (int)HttpStatusCode.OK)]
       public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProduct([FromQuery] PaginationProductQuerys paginationProductQuery) {



            paginationProductQuery.Status = ProductStatus.Activo;
            var paginatioProduct = await _mediator.Send(paginationProductQuery);
            return Ok(paginatioProduct);
        
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "byId")]
        [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetProductByid(int id) {

            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        
        }

        [Authorize(Roles =Role.ADMIN) ]
        [HttpPost("create", Name = "CreateProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> CreateProduct([FromForm]CreateProductCommand request)
        {

            var listFotoUrls = new List<CreateProductImageCommand>();
/*
            foreach(var foto in request.Photos!)
            {
                var resultImage = await _imanageImageService.UploadImage(new Core.Aplication.Models.ImageMangment.ImageData
                {
                    ImageStream = foto.OpenReadStream(),
                    Name = foto.Name
                });

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = resultImage.PublicId,
                    Url = resultImage.Url
                };

                request.ImageUrls = listFotoUrls;
            }*/

            return await _mediator.Send(request);

        }


        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("update", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> UptdateProduct([FromForm] UpdateProductCommand request)
        {

            var listFotoUrls = new List<CreateProductImageCommand>();
            /*
                        foreach(var foto in request.Photos!)
                        {
                            var resultImage = await _imanageImageService.UploadImage(new Core.Aplication.Models.ImageMangment.ImageData
                            {
                                ImageStream = foto.OpenReadStream(),
                                Name = foto.Name
                            });

                            var fotoCommand = new CreateProductImageCommand
                            {
                                PublicCode = resultImage.PublicId,
                                Url = resultImage.Url
                            };

                            request.ImageUrls = listFotoUrls;
                        }*/

            return await _mediator.Send(request);

        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpDelete("updateStatus/{id}", Name = "UpdateStatusProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> UpdateStatusProduct(int id)
        {

            var request = new DeleteProductCommand(id);

            return await _mediator.Send(request);

        }


    }
}
