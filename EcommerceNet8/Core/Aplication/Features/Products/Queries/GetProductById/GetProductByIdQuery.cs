using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductVm>
    {

        public int ProductId { get; set; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentNullException(nameof(productId)):productId;
        }

        

    }
}
