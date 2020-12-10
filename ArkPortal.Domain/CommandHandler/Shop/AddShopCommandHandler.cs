using ArkPortal.Domain.Models.CustomerData;
using System;

namespace ArkPotal.Domain.CommandHandler.Shop
{
    public class AddShopCommand : ICommand
    {
        public Guid AccountId { get; set; }
        public Guid ShopId { get; set; }
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

    public class AddShopCommandHandler : ICommandHandler<AddShopCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<CustomerShop> _repo;

        public AddShopCommandHandler(IUnitOfWork uow, IRepository<CustomerShop> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(AddShopCommand command)
        {

            var InitShop = new CustomerShop
            {
                ShopId = command.ShopId,
                AccountId = command.AccountId,
                ShopAddress = command.ShopAddress,
                ShopEmail = command.ShopEmail,
                ShopName = command.ShopName,
                ShopLink = command.ShopLink,
                ShopPhone = command.ShopPhone,
                ShopCountry = command.ShopCountry,
                ShopRegistrationNumber = command.ShopRegistrationNumber,
                ShopSector = command.ShopSector,
                ShopType = command.ShopType,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now

            };
            _repo.Insert(InitShop);
            _uow.Save();
        }

    }
}
