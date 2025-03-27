using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
   public  class Product : BaseDomainModel
    {

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Name { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "NVARCHAR(4000)")]
        public string? Description { get; set; }

        public int Raiting { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string? Seller { get; set; }

        public int Stokc { get; set; }

        public ProductStatus Status { get; set; } = ProductStatus.Activo;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }


        public virtual ICollection<Review>? reviews { get; set; }

        public virtual ICollection<Image>? images { get; set; }

    }

}
