using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using ArkPotal.Domain.Models.Payments;
using System;

namespace ArkPortal.Domain.QueryHandler.Payments
{
    public class GetPaymentByIdQuery : IQuery<PaymentDetail>
    {
        public Guid PaymentDetailId { get; set; }
    }

    public class GetPaymentByIdQueryHandler : IQueryHandler<GetPaymentByIdQuery, PaymentDetail>
    {
        private readonly IRepository<PaymentDetail> _repo;

        public GetPaymentByIdQueryHandler(IRepository<PaymentDetail> repo)
        {
            _repo = repo;
        }
        public PaymentDetail Handle(GetPaymentByIdQuery query)
        {
            return _repo.GetByID(query.PaymentDetailId);
        }
    }
}
