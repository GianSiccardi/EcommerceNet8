using EcommerceNet8.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
   public class Review : BaseDomainModel
    {

        [Column(TypeName = "NVARCHAR(100)")]
        public string?Name { get; set; }

        public int Rating { get; set; }


        [Column(TypeName = "NVARCHAR(4000)")]
        public  string? Comment { get; set; }
       
        public int ProductId { get; set; }
        
        public virtual Product? product { get; set; }

    }
}
