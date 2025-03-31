using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.PaginationProdcuts
{
    public class PaginationProductQueryHandler : IRequestHandler<PaginationProductQuerys , PaginationVm<ProductVm>>
    {
    }
}
