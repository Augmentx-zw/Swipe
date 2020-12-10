using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using System;

namespace ArkPortal.Domain.QueryHandler.Shop
{
    public class GetShopByAccountIdQuery : IQuery<CustomerShop>
    {
        public Guid AccountId { get; set; }
    }

    public class GetShopAccountByIdQueryHandler : IQueryHandler<GetShopByAccountIdQuery, CustomerShop>
    {
        private readonly IRepository<CustomerShop> _repo;

        public GetShopAccountByIdQueryHandler(IRepository<CustomerShop> repo)
        {
            _repo = repo;
        }
        public CustomerShop Handle(GetShopByAccountIdQuery query)
        {
            return _repo.GetOne(a=> a.AccountId == query.AccountId);
        }
    }
}
