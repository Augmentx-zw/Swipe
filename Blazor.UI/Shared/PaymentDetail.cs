using System;
using System.ComponentModel.DataAnnotations;

namespace Blazor.UI.Shared
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
        public string PaymentStatus { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentEmail { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
