using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArkPortal.Domain.Models.Security
{
    public class PrivateKey
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ShopId { get; set; }
        public Guid Key { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
