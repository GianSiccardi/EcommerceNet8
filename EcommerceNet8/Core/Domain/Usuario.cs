using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNet8.Core.Domain
{
   public class Usuario : IdentityUser
    {
        public string? Name{ get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? AvatarUrl { get; set; }

        public string? IsActive { get; set; }

    }
}
