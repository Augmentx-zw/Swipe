using ArkPortal.Domain.Models.CustomerData;
using System;

namespace ArkPotal.Domain.CommandHandler.BankDetails
{
    public class AddBankDetailCommand : ICommand
    {
        public Guid ShopId { get; set; }
        public Guid BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public string AccountName { get; set; }
        public string AccountPhone { get; set; }
        public string AccountEmail { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class AddBankDetailCommandHandler : ICommandHandler<AddBankDetailCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<CustomerBank> _repo;

        public AddBankDetailCommandHandler(IUnitOfWork uow, IRepository<CustomerBank> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(AddBankDetailCommand command)
        {

            var InitBank = new CustomerBank
            {
                BankId = command.BankId,
                ShopId = command.ShopId,
                AccountName = command.AccountName,
                AccountEmail = command.AccountEmail,
                AccountNumber = command.AccountNumber,
                AccountPhone = command.AccountPhone,
                Branch = command.Branch,
                BankName = command.BankName,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now

            };
            _repo.Insert(InitBank);
            _uow.Save();
        }

    }
}
