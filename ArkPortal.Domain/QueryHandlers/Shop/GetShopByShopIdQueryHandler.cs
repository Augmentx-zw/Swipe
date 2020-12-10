using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using System;

namespace ArkPortal.Domain.QueryHandler.Shop
{
    public class GetShopByShopIdQuery : IQuery<CustomerShop>
    {
        public Guid ShopId { get; set; }
    }
    public class GetShopByShopIdQueryHandler : IQueryHandler<GetShopByShopIdQuery, CustomerShop>
    {
        private readonly IRepository<CustomerShop> _repo;

        public GetShopByShopIdQueryHandler(IRepository<CustomerShop> repo)
        {
            _repo = repo;
        }
        public CustomerShop Handle(GetShopByShopIdQuery query)
        {
            return _repo.GetByID(query.ShopId);
        }
    }
}
