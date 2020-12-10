using ArkPortal.Domain.Models.CustomerData;
using System;

namespace ArkPotal.Domain.CommandHandler.Shop
{
    public class UpDateShopCommand : ICommand
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopEmail { get; set; }
        public string ShopPhone { get; set; }
        public string ShopType { get; set; }
        public string ShopLink { get; set; }
        public string ShopSector { get; set; }
        public string ShopCountry { get; set; }
        public string ShopAddress { get; set; }
        public string ShopRegistrationNumber { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
    public class UpDateShopCommandHandler : ICommandHandler<UpDateShopCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<CustomerShop> _repo;

        public UpDateShopCommandHandler(IUnitOfWork uow, IRepository<CustomerShop> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(UpDateShopCommand command)
        {
            var uShop = _repo.GetByID(command.ShopId);
            uShop.ShopAddress = command.ShopAddress;
            uShop.ShopEmail = command.ShopEmail;
            uShop.ShopLink = command.ShopLink;
            uShop.ShopName = command.ShopName;
            uShop.ShopPhone = command.ShopPhone;
            uShop.ShopCountry = command.ShopCountry;
            uShop.ShopRegistrationNumber = command.ShopRegistrationNumber;
            uShop.ShopSector = command.ShopSector;
            uShop.ShopType = command.ShopType;
            uShop.UpdatedOn = DateTime.Now;
            _uow.Save();
        }

    }
}
