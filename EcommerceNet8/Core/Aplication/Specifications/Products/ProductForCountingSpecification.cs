using EcommerceNet8.Core.Domain;

namespace EcommerceNet8.Core.Aplication.Specifications.Products
{
    public class ProductForCountingSpecification : BaseSpecification<Product>
    {

        public ProductForCountingSpecification(ProductSpecificationParams productParams)
           : base(x =>
               (string.IsNullOrEmpty(productParams.Search) ||
                x.Name!.Contains(productParams.Search) ||
                x.Description!.Contains(productParams.Search)) &&
               (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
               (!productParams.PrecioMin.HasValue || x.Price >= productParams.PrecioMin) &&
               (!productParams.PrecioMax.HasValue || x.Price <= productParams.PrecioMax) &&
               (!productParams.Status.HasValue || x.Status == productParams.Status))
        {
        }
    }
}
