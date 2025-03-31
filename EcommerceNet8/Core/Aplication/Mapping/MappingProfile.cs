using AutoMapper;
using EcommerceNet8.Core.Aplication.Features.Image.Queries;
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

            CreateMap<Image, ImageVm>();
            CreateMap<Review, ReviewVm>();

        }

    }
}
