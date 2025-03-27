using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
    public class ShoppingCart : BaseDomainModel
     {

        public Guid? ShoppingCartMasterId { get; set; }
    
        public virtual ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }




    }
}
