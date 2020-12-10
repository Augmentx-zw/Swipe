using System;
using System.ComponentModel.DataAnnotations;

namespace Blazor.UI.Shared
{
    public class CustomerShop
    {
        [Key]
        public Guid ShopId { get; set; }
        public Guid AccountId { get; set; }
        public string ShopName { get; set; }
        public string ShopEmail { get; set; }
        public string ShopPhone { get; set; }
        public string ShopType { get; set; }
        public string ShopLink { get; set; }
        public string ShopSector { get; set; }
        public string ShopAddress { get; set; }
        public string ShopCountry { get; set; }
        public string ShopRegistrationNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
