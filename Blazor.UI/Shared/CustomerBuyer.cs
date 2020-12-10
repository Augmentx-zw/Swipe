using System;

namespace Blazor.UI.Shared
{
    public class CustomerBuyer
    {
        public Guid BuyerId { get; set; }
        public Guid PaymentDetailId { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
