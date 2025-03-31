using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;
using System.Linq.Expressions;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProductListQueryHandler(IUnitOfWork unitOfWork ,IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IReadOnlyList<ProductVm>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();

            includes.Add(p => p.images!);
            includes.Add(p => p.reviews!);

            var products = await _unitOfWork.Repository<Product>().GetAsync(
                null,
                x => x.OrderBy(y => y.Name),
                includes,
                true
            ); // Punto y coma fuera, después de cerrar el método


            var productsVm = _mapper.Map<IReadOnlyList<ProductVm>>(products);

            return productsVm;
        }
    }
}
