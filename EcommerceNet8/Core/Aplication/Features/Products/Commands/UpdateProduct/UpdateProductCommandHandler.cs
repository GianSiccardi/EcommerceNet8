using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Core.Domain;
using MediatR;
using EcommerceNet8.Core.Aplication.Exceptions;

namespace EcommerceNet8.Core.Aplication.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);

            if(productToUpdate is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request.ToDto(), productToUpdate);
            await _unitOfWork.Repository<Product>().UpdateAsync(productToUpdate);

          /*  if((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
            {
                var imagesToRemove = await _unitOfWork.Repository<Imagee>().GetAsync(
                    x => x.ProductId == request.Id
                    );

                _unitOfWork.Repository<Imagee>().DeleteRange(imagesToRemove);

                request.ImageUrls.Select(c => { c.ProductId = request.Id; return c; }).ToList();

                var images = _mapper.Map<List<Imagee>>(request.ImageUrls);
                _unitOfWork.Repository<Imagee>().AddRange(images);


                await _unitOfWork.Complete();
            }*/

            return _mapper.Map<ProductVm>(productToUpdate);
           
        }
    }
}
