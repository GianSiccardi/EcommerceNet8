
using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
   public class Adress : BaseDomainModel
    {
        public string? Addres { get; set; }
        public string? City { get; set; }
        public string? Department { get; set; }
        public string? PostalCode { get; set; }
        public string? Username { get; set; }
        public string? Country { get; set; }

    }
}
