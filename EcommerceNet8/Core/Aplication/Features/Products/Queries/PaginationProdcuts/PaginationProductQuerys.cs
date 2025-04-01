using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.PaginationProdcuts
{
    public class PaginationProductQuerys : PaginationBaseQuerys , IRequest <PaginationVm<ProductVm>>
    {

        
        public int? CategoryId { get; set; }

        public  decimal?  PriceMax { get; set; }

        public decimal? PriceMin { get; set; }

        public int? Raiting { get; set; }


        public ProductStatus? Status { get; set; }


    }
}
