using ArkPotal.Domain.Models.Payments;
using System;

namespace ArkPotal.Domain.CommandHandler.InitiatePayment
{
    public class PemDeletePaymentCommand : ICommand
    {
        public Guid PaymentDetailId { get; set; }
    }
    public class PemDeletePaymentCommandHandler : ICommandHandler<PemDeletePaymentCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<PaymentDetail> _repo;

        public PemDeletePaymentCommandHandler(IUnitOfWork uow, IRepository<PaymentDetail> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(PemDeletePaymentCommand command)
        {
            var initPayment = _repo.GetByID(command.PaymentDetailId);
            _repo.Delete(initPayment);
            _uow.Save();

        }

    }
}
