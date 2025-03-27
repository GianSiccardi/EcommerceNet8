using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
    public class  Country : BaseDomainModel 
    {

        public string? Name { get; set; }

        public string? Iso2 { get; set; }

        public string? Iso3 { get; set; }
    }
}
