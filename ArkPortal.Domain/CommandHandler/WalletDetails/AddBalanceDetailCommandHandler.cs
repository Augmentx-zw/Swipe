using ArkPortal.Domain.Models.CustomerData;
using System;

namespace ArkPotal.Domain.CommandHandler.WalletDetails
{
    public class AddBalanceDetailCommand : ICommand
    {
        public Guid ShopId { get; set; }
        public Guid BalanceId { get; set; }
        public double OldBalance { get; set; }
        public double NewBalance { get; set; }
        public string ActionMade { get; set; }
        public string StatusMessage { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class AddBalanceDetailCommandHandler : ICommandHandler<AddBalanceDetailCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<CustomerWallet> _repo;

        public AddBalanceDetailCommandHandler(IUnitOfWork uow, IRepository<CustomerWallet> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(AddBalanceDetailCommand command)
        {

            var wallet = new CustomerWallet
            {
                BalanceId = command.BalanceId,
                ShopId = command.ShopId,
                OldBalance = command.OldBalance,
                NewBalance = command.NewBalance,
                ActionMade = command.ActionMade,
                StatusMessage = command.StatusMessage,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now

            };
            _repo.Insert(wallet);
            _uow.Save();
        }

    }
}
