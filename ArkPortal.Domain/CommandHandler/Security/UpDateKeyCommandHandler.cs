using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.Models.Security;
using System;

namespace ArkPotal.Domain.CommandHandler.Security
{
    public class UpDateKeyCommand : ICommand
    {
        public Guid ShopId { get; set; }
        public Guid Key { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
    public class UpDateKeyCommandHandler : ICommandHandler<UpDateKeyCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<PrivateKey> _repo;

        public UpDateKeyCommandHandler(IUnitOfWork uow, IRepository<PrivateKey> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(UpDateKeyCommand command)
        {
            var uKey = _repo.GetOne(a=> a.ShopId == command.ShopId);
            uKey.Key = Guid.NewGuid();
            uKey.UpdatedOn = DateTime.Now;
            _uow.Save();
        }

    }
}
