using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using System;
using System.Collections.Generic;

namespace ArkPortal.Domain.QueryHandler.WalletDetails
{
    public class GetAllBalancesQueryByShopId : IQuery<IEnumerable<CustomerWallet>>
    {
        public Guid ShopId { get; set; }
    }
    public class GetAllBalancesQueryByShopIdHandler : IQueryHandler<GetAllBalancesQueryByShopId, IEnumerable<CustomerWallet>>
    {
        private readonly IRepository<CustomerWallet> _repo;

        public GetAllBalancesQueryByShopIdHandler(IRepository<CustomerWallet> repo)
        {
            _repo = repo;
        }
        public IEnumerable<CustomerWallet> Handle(GetAllBalancesQueryByShopId query)
        {
            return _repo.Get(a => a.ShopId == query.ShopId);
        }
    }

}