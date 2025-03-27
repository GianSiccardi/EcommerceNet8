using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceNet8.Core.Domain.Common
{
    public abstract class BaseDomainModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public DateTime? LastModifiedDataBy { get; set; }

        public int ProductId { get; set; }


        public int Stock { get; set; }
    }
}

