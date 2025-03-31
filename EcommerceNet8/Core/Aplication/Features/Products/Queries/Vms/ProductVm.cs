using EcommerceNet8.Core.Aplication.Features.Image.Queries;
using EcommerceNet8.Core.Aplication.Features.Reviews.Queries.Vms;
using EcommerceNet8.Core.Aplication.Models.Product;
using EcommerceNet8.Core.Domain;

namespace EcommerceNet8.Core.Aplication.Features.Products.Queries.Vms
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public int Raiting { get; set; }

        public string? seller { get; set; }


        public virtual ICollection<ReviewVm>? Reviews { get; set; }


        public virtual ICollection<ImageVm>? Images { get; set; }

        public int CategoryId { get; set; }


        public string? CategoryName { get; set; }

        public int NroReviews { get; set; }

        public ProductStatus Status { get; set; }

        public string StatusLabe
        {
            get
            {
                switch (Status)
                {
                    case ProductStatus.Activo:
                        {
                            return ProductoStatusLabel.ACTIVE;
                        }

                    case ProductStatus.Inactivo:
                        {
                            return ProductoStatusLabel.INACTIVE;
                        }

                    default: return ProductoStatusLabel.INACTIVE;
                }

            }
            set { }
        }


        }

    }

