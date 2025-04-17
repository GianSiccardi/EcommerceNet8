using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<ProductVm>
    {
        public  int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentException(nameof(productId)): productId;
        }
    }
}
