using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
    public enum OrderStatus
    {

        [EnumMember(Value ="Pendiente")]
        Pending ,
        [EnumMember(Value = "Pago Completado")]
        Completed ,

        [EnumMember(Value = "El producto fue enviado")]
        Sent,


       [EnumMember(Value = "Ocurrio un error")]
        Error
    }
}
