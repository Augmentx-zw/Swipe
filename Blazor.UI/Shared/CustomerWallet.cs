using System;
using System.ComponentModel.DataAnnotations;

namespace Blazor.UI.Shared
{
    public class CustomerWallet
    {
        [Key]
        public Guid BalanceId { get; set; }
        public Guid ShopId { get; set; }
        public double OldBalance { get; set; }
        public double NewBalance { get; set; }
        public string ActionMade { get; set; }
        public string StatusMessage { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
