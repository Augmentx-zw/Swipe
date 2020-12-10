using ArkPortal.Domain.Models.CustomerData;
using System;

namespace ArkPotal.Domain.CommandHandler.Buyer
{
    public class AddBuyerCommand : ICommand
    {
        public Guid BuyerId { get; set; }
        public Guid PaymentDetailId { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class AddBuyerCommandHandler : ICommandHandler<AddBuyerCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<CustomerBuyer> _repo;

        public AddBuyerCommandHandler(IUnitOfWork uow, IRepository<CustomerBuyer> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(AddBuyerCommand command)
        {

            var InitBuyer = new CustomerBuyer
            {
                BuyerId = Guid.NewGuid(),
                BuyerEmail = command.BuyerEmail,
                BuyerPhone = command.BuyerPhone,
                PaymentDetailId = command.PaymentDetailId,
                CreatedOn = DateTime.Now
            };
            _repo.Insert(InitBuyer);
            _uow.Save();
        }

    }
}
