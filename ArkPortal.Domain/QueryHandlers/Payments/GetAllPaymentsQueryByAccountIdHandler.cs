using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using ArkPotal.Domain.Models.Payments;
using System;
using System.Collections.Generic;

namespace ArkPortal.Domain.QueryHandler.Payments
{
    public class GetAllPaymentsQueryByAccountId : IQuery<IEnumerable<PaymentDetail>>
    {
        public Guid ShopId { get; set; }
    }
    public class GetAllPaymentsQueryByAccountIdHandler : IQueryHandler<GetAllPaymentsQueryByAccountId, IEnumerable<PaymentDetail>>
    {
        private readonly IRepository<PaymentDetail> _repo;

        public GetAllPaymentsQueryByAccountIdHandler(IRepository<PaymentDetail> repo)
        {
            _repo = repo;
        }
        public IEnumerable<PaymentDetail> Handle(GetAllPaymentsQueryByAccountId query)
        {
            return _repo.Get(a => a.ShopId == query.ShopId && !a.IsDeleted);
        }
    }

}