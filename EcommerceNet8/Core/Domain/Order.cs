using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
  public  class Order : BaseDomainModel
    {

        public Order() { }

        public Order(
      string buyerName,
      string buyerUsername,
      OrderAddres orderAddress,
      decimal subTotal,
      decimal total,
      decimal tax,
      decimal annvioPrice,
      List<OrderItems> orderItems // Se recibe como List para poder modificarla internamente
  )
        {
            BuyerName = buyerName;
            BuyerUsername = buyerUsername;
            OrderAddres = orderAddress;
            SubTotal = subTotal;
            Total = total;
            Tax = tax;
            AnnvioPrice = annvioPrice;
            orderItems = orderItems ?? new List<OrderItems>(); // Se inicializa la lista para evitar null
        }

        public string? BuyerName { get; set; }

        public string? BuyerUsername { get; set; }

        public OrderAddres? OrderAddres { get; set; }

        public IReadOnlyList <OrderItems>? OrderItems { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Tax { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AnnvioPrice { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

      

        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; }


        public string? StripeApiKey { get; set; }

    }
}
