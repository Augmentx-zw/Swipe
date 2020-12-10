using System;
using System.ComponentModel.DataAnnotations;

namespace ArkPotal.Domain.Models.Payments
{
    public class PaymentDetail
    {

        [Key]
        public Guid PaymentDetailId { get; set; }
        public Guid ShopId { get; set; }
        public Guid TransactionReference { get; set; }
        public string CurrencyCode { get; set; }
        public double Amount { get; set; }
        public string ShopName { get; set; }
        public string Optional { get; set; }
        public string ErrorUrl { get; set; }
        public string SuccessUrl { get; set; }
        public string HashCode { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentEmail { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
