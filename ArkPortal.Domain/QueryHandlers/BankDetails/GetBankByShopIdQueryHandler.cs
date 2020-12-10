using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using System;

namespace ArkPortal.Domain.QueryHandler.BankDetails
{
    public class GetBankByShopIdQuery : IQuery<CustomerBank>
    {
        public Guid ShopId { get; set; }
    }

    public class GetBankByShopIdQueryHandler : IQueryHandler<GetBankByShopIdQuery, CustomerBank>
    {
        private readonly IRepository<CustomerBank> _repo;

        public GetBankByShopIdQueryHandler(IRepository<CustomerBank> repo)
        {
            _repo = repo;
        }
        public CustomerBank Handle(GetBankByShopIdQuery query)
        {
            return _repo.GetOne(a => a.ShopId == query.ShopId);
        }
    }
}
