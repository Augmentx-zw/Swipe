using ArkPortal.Domain.Models.CustomerData;
using System;

namespace ArkPotal.Domain.CommandHandler.WalletDetails
{
    public class UpDateBalanceDetailCommand : ICommand
    {
        public Guid BalanceId { get; set; }
        public string ActionMade { get; set; }
        public string StatusMessage { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
    public class UpDateBalanceDetailCommandHandler : ICommandHandler<UpDateBalanceDetailCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<CustomerWallet> _repo;

        public UpDateBalanceDetailCommandHandler(IUnitOfWork uow, IRepository<CustomerWallet> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(UpDateBalanceDetailCommand command)
        {
            var wallet = _repo.GetByID(command.BalanceId);
            wallet.ActionMade = command.ActionMade;
            wallet.StatusMessage = command.StatusMessage;
            wallet.UpdatedOn = DateTime.Now;
            _uow.Save();
        }

    }
}
