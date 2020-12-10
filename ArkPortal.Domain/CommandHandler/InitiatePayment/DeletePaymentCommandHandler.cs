using ArkPotal.Domain.Models.Payments;
using System;

namespace ArkPotal.Domain.CommandHandler.InitiatePayment
{
    public class DeletePaymentCommand : ICommand
    {
        public Guid PaymentDetailId { get; set; }
    }
    public class DeletePaymentCommandHandler : ICommandHandler<DeletePaymentCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<PaymentDetail> _repo;

        public DeletePaymentCommandHandler(IUnitOfWork uow, IRepository<PaymentDetail> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(DeletePaymentCommand command)
        {
            var InitPayment = _repo.GetByID(command.PaymentDetailId);
            InitPayment.IsDeleted = true;
            InitPayment.UpdatedOn = DateTime.Now;
            _uow.Save();
        }

    }
}
