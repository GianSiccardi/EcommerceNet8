using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductVm>
    {
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string Seller { get; set; }

        public int Stock { get; set; }

        public string?  CategoryId { get; set; }

        public IReadOnlyList<IFormFile>? Photos { get; set; }

        public IReadOnlyList<CreateProductImageCommand>? ImageUrls;

    }
}
