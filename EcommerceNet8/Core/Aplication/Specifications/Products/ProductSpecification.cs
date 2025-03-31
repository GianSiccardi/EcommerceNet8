using EcommerceNet8.Core.Domain;

namespace EcommerceNet8.Core.Aplication.Specifications.Products
{
    public class ProductSpecification :BaseSpecification<Product>
    {

        public ProductSpecification(ProductSpecificationParams productParams) :
             base(x =>
               (string.IsNullOrEmpty(productParams.Search) ||
                x.Name!.Contains(productParams.Search) ||
                x.Description!.Contains(productParams.Search)) &&
               (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
               (!productParams.PrecioMin.HasValue || x.Price >= productParams.PrecioMin) &&
               (!productParams.PrecioMax.HasValue || x.Price <= productParams.PrecioMax) &&
               (!productParams.Status.HasValue || x.Status == productParams.Status))
            
        {

            AddInclude(p=> p.reviews!);
            AddInclude(p => p.images!);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBY(p => p.Name!);
                        break;


                    case "nameDesc":
                        AddOrderBYDescending(p => p.Name!);
                        break;

                    case "precioAsc":
                        AddOrderBY(p => p.Price!);
                        break;


                    case "precioDesc":
                        AddOrderBYDescending(p => p.Price!);
                        break;



                    case "ratingAsc":
                        AddOrderBY(p => p.Raiting!);
                        break;


                    case "ratingDesc":
                        AddOrderBYDescending(p => p.Raiting!);
                        break;


                    default:
                        AddOrderBY(p => p.CreatedDate!);
                        break;





                } 
            }
            else
            {
                AddOrderBYDescending(p => p.CreatedDate!);
            }

        }
    }
}
