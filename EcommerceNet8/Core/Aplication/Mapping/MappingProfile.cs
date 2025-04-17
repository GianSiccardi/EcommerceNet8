using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Categories.VmS;
using EcommerceNet8.Core.Aplication.Features.Countries.Vms;
using EcommerceNet8.Core.Aplication.Features.Image.Queries;
using EcommerceNet8.Core.Aplication.Features.Products.Commands.CreateProduct.Dto;
using EcommerceNet8.Core.Aplication.Features.Products.Commands.UpdateProduct;
using EcommerceNet8.Core.Aplication.Features.Products.Commands.UpdateProduct.Dto;
using EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms;
using EcommerceNet8.Core.Aplication.Features.Reviews.Queries.Vms;
using EcommerceNet8.Core.Domain;

namespace EcommerceNet8.Core.Aplication.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductVm>()
                .ForMember(p => p.CategoryName, x => x.MapFrom(a => a.Category!.Name))
                .ForMember(p => p.NroReviews, x => x.MapFrom(a => a.reviews == null ? 0 : a.reviews.Count));

            CreateMap<Imagee, ImageVm>();
            CreateMap<Review, ReviewVm>()
                .ForMember(r => r.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Country, CountryVm>();
            CreateMap<Category, CategoryVm>();
            CreateMap<UpdateProductDto, Product>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id)) // Corregido
                    .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(p => p.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(p => p.Seller, opt => opt.MapFrom(src => src.Seller))
                    .ForMember(p => p.Stock, opt => opt.MapFrom(src => src.Stock))
    .ForMember(p => p.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(p => p.Status, opt => opt.MapFrom(src => ProductStatus.Activo))
                    .ForMember(p => p.Raiting, opt => opt.Ignore())
                    .ForMember(p => p.reviews, opt => opt.Ignore())
                    .ForMember(p => p.images, opt => opt.Ignore())
                    .ForMember(p => p.Category, opt => opt.Ignore())
                    .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                    .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                    .ForMember(p => p.LastModifiedDate, opt => opt.Ignore())
                    .ForMember(p => p.LastModifiedDataBy, opt => opt.Ignore());

            CreateMap<CreateProductCommandDto, Product>()
    .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
    .ForMember(p => p.Price, opt => opt.MapFrom(src => src.Price))
    .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
    .ForMember(p => p.Seller, opt => opt.MapFrom(src => src.Seller))
    .ForMember(p => p.Stock, opt => opt.MapFrom(src => src.Stock))
    .ForMember(p => p.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
    .ForMember(p => p.Status, opt => opt.MapFrom(src => ProductStatus.Activo))
    .ForMember(p => p.Raiting, opt => opt.Ignore())
    .ForMember(p => p.reviews, opt => opt.Ignore())
    .ForMember(p => p.images, opt => opt.Ignore())
    .ForMember(p => p.Category, opt => opt.Ignore());

        }



        

    }
}
