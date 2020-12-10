using ArkPortal.Domain.Models.Security;
using System;

namespace ArkPotal.Domain.CommandHandler.Security
{
    public class AddKeyCommand : ICommand
    {
        public Guid ShopId { get; set; }
        public Guid Key { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class AddKeyCommandHandler : ICommandHandler<AddKeyCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<PrivateKey> _repo;

        public AddKeyCommandHandler(IUnitOfWork uow, IRepository<PrivateKey> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(AddKeyCommand command)
        {

            PrivateKey InitKey = new PrivateKey
            {
                ShopId = command.ShopId,
                Key = command.Key,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now

            };
            _repo.Insert(InitKey);
            _uow.Save();
        }

    }
}
