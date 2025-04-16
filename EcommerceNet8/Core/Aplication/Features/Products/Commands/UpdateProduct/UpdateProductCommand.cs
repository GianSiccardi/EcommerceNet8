using EcommerceNet8.Core.Aplication.Features.Products.Commands.CreateProduct.Dto;
using EcommerceNet8.Core.Aplication.Features.Products.Commands.UpdateProduct.Dto;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductVm>
    {

        public int Id { get; set; }
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string Seller { get; set; }

        public int Stock { get; set; }

        public string? CategoryId { get; set; }

        // public IReadOnlyList<IFormFile>? Photos { get; set; }

        //   public IReadOnlyList<CreateProductImageCommand>? ImageUrls;

        public UpdateProductDto ToDto() => new UpdateProductDto
        {
            Name = Name,
            Price = Price,
            Description = Description,
            Seller = Seller,
            Stock = Stock,
            CategoryId = CategoryId,
            Id=Id 
        };
    }
}
