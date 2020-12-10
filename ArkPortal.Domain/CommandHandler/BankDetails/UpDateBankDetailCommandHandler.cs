using ArkPortal.Domain.Models.CustomerData;
using System;

namespace ArkPotal.Domain.CommandHandler.BankDetails
{
    public class UpDateBankDetailCommand : ICommand
    {
        public Guid BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public string AccountName { get; set; }
        public string AccountPhone { get; set; }
        public string AccountEmail { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
    public class UpDateBankDetailCommandHandler : ICommandHandler<UpDateBankDetailCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<CustomerBank> _repo;

        public UpDateBankDetailCommandHandler(IUnitOfWork uow, IRepository<CustomerBank> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(UpDateBankDetailCommand command)
        {
            var uBank = _repo.GetByID(command.BankId);
            uBank.BankName = command.BankName;
            uBank.Branch = command.Branch;
            uBank.AccountPhone = command.AccountPhone;
            uBank.AccountNumber = command.AccountNumber;
            uBank.AccountName = command.AccountName;
            uBank.AccountEmail = command.AccountEmail;
            uBank.UpdatedOn = DateTime.Now;
            _uow.Save();
        }

    }
}
