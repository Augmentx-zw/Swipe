using ArkPotal.Domain.Models.Payments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArkPotal.Domain.CommandHandler.InitiatePayment
{
    public class AddInitiatePaymentCommand : ICommand
    {
        public Guid ShopId { get; set; }
        public Guid PaymentDetailId { get; set; }
        public string PrivateKey { get; set; }
        public string CurrencyCode { get; set; }
        public double Amount { get; set; }
        public string ShopName { get; set; }
        public string Optional { get; set; }
        public string ErrorUrl { get; set; }
        public string SuccessUrl { get; set; }
        public string HashCode { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }

    public class AddInitiatePaymentCommandHandler : ICommandHandler<AddInitiatePaymentCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<PaymentDetail> _repo;
        private readonly ISecurityUtils _sec;

        public AddInitiatePaymentCommandHandler(IUnitOfWork uow, IRepository<PaymentDetail> repo, ISecurityUtils sec)
        {
            _uow = uow;
            _repo = repo;
            _sec = sec;
        }
        public void Handle(AddInitiatePaymentCommand command)
        {
            var hashCode = GethashStatus(command, command.PrivateKey);
            if (hashCode != command.HashCode)
            {
                throw new ArgumentException("Hashcode is not correct, so we do not recognise payment");
            }
            var nInitPayment = new PaymentDetail
            {
                PaymentDetailId = command.PaymentDetailId,
                ShopId = command.ShopId,
                CurrencyCode = command.CurrencyCode,
                Amount = command.Amount,
                ShopName = command.ShopName,
                TransactionReference = Guid.NewGuid(),
                Optional = command.Optional,
                ErrorUrl = command.ErrorUrl,
                SuccessUrl = command.SuccessUrl,
                HashCode = hashCode,
                PaymentStatus = "Created",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
            };
            _repo.Insert(nInitPayment);
            _uow.Save();
        }
        private string GethashStatus(AddInitiatePaymentCommand command, string privateKey)
        {
            var postFields = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("CurrencyCode", command.CurrencyCode),
            new KeyValuePair<string, string>("Amount", command.Amount.ToString()),
            new KeyValuePair<string, string>("Optional", command.Optional),
            new KeyValuePair<string, string>("ErrorUrl", command.ErrorUrl),
            new KeyValuePair<string, string>("SuccessUrl", command.SuccessUrl),
            new KeyValuePair<string, string>("ShopName", command.ShopName),
            new KeyValuePair<string, string>("ShopId", command.ShopId.ToString())


        };

            var hashCheck = _sec.GetSHA512Hash(postFields.Select(x => x.Value).ToArray().Concat(new[] { privateKey }).ToArray());

            return hashCheck;
        }
    }
}
