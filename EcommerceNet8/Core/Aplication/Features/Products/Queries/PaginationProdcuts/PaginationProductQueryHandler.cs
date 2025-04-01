using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Features.Shared.Queries;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Aplication.Specifications.Products;
using EcommerceNet8.Core.Domain;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.PaginationProdcuts
{
    public class PaginationProductQueryHandler : IRequestHandler<PaginationProductQuerys, PaginationVm<ProductVm>>

    {

        // gracias al IRequestHandler<PaginationProductQuerys, PaginationVm<ProductVm>> , el medaitor sabe que debe venir a este handler ,porque le paso PaginationProductQuerys por parametros
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public PaginationProductQueryHandler(IUnitOfWork unitoWork  ,IMapper mapper)
        {
            _unitOfWork = unitoWork;
            _mapper = mapper;
            
            
        }
        public async Task<PaginationVm<ProductVm>> Handle(PaginationProductQuerys request, CancellationToken cancellationToken)
        {
            var productSpecificationParams = new ProductSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                CategoryId=request.CategoryId,
                PrecioMax = request.PriceMax,
                PrecioMin = request.PriceMin,
                Rating = request.Raiting,
                Status = request.Status
            };

            var spec = new ProductSpecification(productSpecificationParams);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);

            var spectCount = new ProductForCountingSpecification(productSpecificationParams);
            var totalProducts = await _unitOfWork.Repository<Product>().CountAsync(spectCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(request.PageSize));
            var totalPage = Convert.ToInt32(rounded);

            var data=_mapper.Map<IReadOnlyList<ProductVm>>(products);

            var productsByPage = products.Count();


            var pagination = new PaginationVm<ProductVm>
            {
                Count = totalProducts,
                Data=data,
                PageCount=totalPage,
                PageIndex=request.PageIndex,
                PageSize=request.PageSize,
                ResultByPage=productsByPage
                ,

            };

            return pagination;

        }

    }
}
