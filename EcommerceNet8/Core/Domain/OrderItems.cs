using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
    public class OrderItems:BaseDomainModel
    {

        public Product? Product { get; set; }
        public int ProductId { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }


        public int Quantity { get; set; }

        public Order Order { get; set; }

        public int OrderId { get; set; }

        public int ProductItemId { get; set; }


        public string? ProductName { get; set; }


        public string? ImageUrl { get; set; }
    }
}
