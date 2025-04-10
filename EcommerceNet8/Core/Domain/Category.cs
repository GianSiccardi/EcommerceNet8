﻿using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
   public class Category :BaseDomainModel
    {
        [Column(TypeName= "NVARCHAR(100)")]
        public string? Name { get; set; }
        
        public virtual ICollection <Product>? products { get; set; }


    }
}
