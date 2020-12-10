using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using System;

namespace ArkPortal.Domain.QueryHandler.WalletDetails
{
    public class GetBalanceByIdQuery : IQuery<CustomerWallet>
    {
        public Guid BalanceId { get; set; }
    }

    public class GetBalanceByIdQueryHandler : IQueryHandler<GetBalanceByIdQuery, CustomerWallet>
    {
        private readonly IRepository<CustomerWallet> _repo;

        public GetBalanceByIdQueryHandler(IRepository<CustomerWallet> repo)
        {
            _repo = repo;
        }
        public CustomerWallet Handle(GetBalanceByIdQuery query)
        {
            return _repo.GetByID(query.BalanceId);
        }
    }
}
