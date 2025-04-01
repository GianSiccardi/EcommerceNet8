using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;
using System.Linq.Expressions;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductVm>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public async Task<ProductVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.images!);
            includes.Add(p => p.reviews!.OrderByDescending(x => x.CreatedDate));

            var product = await _unitOfWork.Repository<Product>().GetEntityAsync(x=>x.Id==request.ProductId ,includes,true);

            var response = _mapper.Map<ProductVm>(product);

            return response;
        }
    }
}
