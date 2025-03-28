using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;
using System.Linq.Expressions;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<Product>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;
        }


        public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
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

            return new List<Product>(products);
        }
    }
}
