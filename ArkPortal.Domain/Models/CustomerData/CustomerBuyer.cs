using System;
using System.ComponentModel.DataAnnotations;

namespace ArkPortal.Domain.Models.CustomerData
{
    public class CustomerBuyer
    {
        [Key]
        public Guid BuyerId { get; set; }
        public Guid PaymentDetailId { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
