using EcommerceNet8.Core.Aplication.Models.ImageMangment;

namespace EcommerceNet8.Core.Aplication.Contracts.Infrastructure
{
    public interface IManageImageService
    {
        Task<ImageResponse> UploadImage(ImageData imageStream);
    }
}
