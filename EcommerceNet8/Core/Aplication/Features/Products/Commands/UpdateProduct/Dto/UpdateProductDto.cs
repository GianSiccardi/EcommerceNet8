using EcommerceNet8.Core.Aplication.Features.Products.Commands.CreateProduct;

namespace EcommerceNet8.Core.Aplication.Features.Products.Commands.UpdateProduct.Dto
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Seller { get; set; }
        public int Stock { get; set; }
        public string? CategoryId { get; set; }
        public IReadOnlyList<CreateProductImageCommand>? ImageUrls { get; set; }
    }
}
