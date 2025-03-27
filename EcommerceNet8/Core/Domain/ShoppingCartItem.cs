using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
    public class ShoppingCartItem :BaseDomainModel
    {

        public string? Product { get; set; }

        [Column(TypeName ="decimal(10,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Image { get; set; }

        public string? CategoryName  { get; set; }

        public Guid? ShoppingCartMasterId { get; set; }

        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}
