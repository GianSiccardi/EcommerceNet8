using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using EcommerceNet8.Infraestructure.Repository;
using MediatR;

namespace EcommerceNet8.Core.Aplication.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVm>
    {

        private readonly UnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CreateProductCommandHandler(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);

            await _unitOfWork.Repository<Product>().AddAsync(productEntity);


            if((request.ImageUrls is not null)&& request.ImageUrls.Count> 0)
            {
                request.ImageUrls.Select(c => { c.ProductId = productEntity.Id; return c; }).ToList();

               var images = _mapper.Map<List<Imagee>>(request.ImageUrls);
                _unitOfWork.Repository<Imagee>().AddRange(images);
                await _unitOfWork.Complete();
            }

            return _mapper.Map<ProductVm>(productEntity);
        }
    }
}
