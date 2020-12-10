using System;
using System.ComponentModel.DataAnnotations;

namespace ArkPortal.Domain.Models.CustomerData
{
    public class CustomerBank
    {
        [Key]
        public Guid BankId { get; set; }
        public Guid ShopId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public string AccountName { get; set; }
        public string AccountPhone { get; set; }
        public string AccountEmail { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
