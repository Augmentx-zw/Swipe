using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.Models.Security;
using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using System;

namespace ArkPortal.Domain.QueryHandler.Security
{
    public class GetKeyByShopIdQuery : IQuery<PrivateKey>
    {
        public Guid ShopId { get; set; }
    }
    public class GetKeyByShopIdQueryHandler : IQueryHandler<GetKeyByShopIdQuery, PrivateKey>
    {
        private readonly IRepository<PrivateKey> _repo;

        public GetKeyByShopIdQueryHandler(IRepository<PrivateKey> repo)
        {
            _repo = repo;
        }
        public PrivateKey Handle(GetKeyByShopIdQuery query)
        {
            return _repo.GetOne(p => p.ShopId ==query.ShopId);
        }
    }
}
