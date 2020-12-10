using ArkPotal.Domain.Models.Payments;
using System;

namespace ArkPotal.Domain.CommandHandler.InitiatePayment
{
    public class UpDatePaymentCommand : ICommand
    {
        public Guid PaymentDetailId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentEmail { get; set; }


    }
    public class UpDatePaymentCommandHandler : ICommandHandler<UpDatePaymentCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<PaymentDetail> _repo;

        public UpDatePaymentCommandHandler(IUnitOfWork uow, IRepository<PaymentDetail> repo)
        {
            _uow = uow;
            _repo = repo;
        }
        public void Handle(UpDatePaymentCommand command)
        {
            var initPayment = _repo.GetByID(command.PaymentDetailId);
            string res = ProccessPayment();
            if (!initPayment.IsDeleted)
            {
                initPayment.PaymentStatus = res;
                initPayment.PaymentNumber = command.PaymentNumber;
                initPayment.PaymentEmail = command.PaymentEmail;
                initPayment.PaymentMethod = command.PaymentMethod;
                initPayment.UpdatedOn = DateTime.Now;
                _uow.Save();
            }
        }

        public string ProccessPayment()
        {
            string message = "";
            Random rnd = new Random();
            int i = rnd.Next(4);
            if (i == 0)
            {
                message = "User cancelled transaction";
            }
            else if (i == 1)
            {
                message = "Transacton Rollback, failed to process";
            }
            else if (i == 2)
            {
                message = "Failure to communicate with payment provider transaction cancelled";
            }
            else if (i == 3)
            {
                message = "Transaction Proccessd";
            }


            return message;
        }

    }
}
