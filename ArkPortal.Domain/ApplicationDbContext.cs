using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.Models.Security;
using ArkPotal.Domain.Models.Payments;
using Microsoft.EntityFrameworkCore;

namespace ArkPotal.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<CustomerShop> CustomerShops { get; set; }
        public DbSet<CustomerBuyer> CustomerBuyers { get; set; }
        public DbSet<CustomerBank> CustomerBanks { get; set; }
        public DbSet<CustomerWallet> CustomerWallets { get; set; }
        public DbSet<PrivateKey> PrivateKey { get; set; }

    }
}
