using ArkPortal.Domain.Models.CustomerData;
using ArkPortal.Domain.QueryHandlers;
using ArkPotal.Domain;
using System;

namespace ArkPortal.Domain.QueryHandler.BankDetails
{
    public class GetBankByBankIdQuery : IQuery<CustomerBank>
    {
        public Guid BankId { get; set; }
    }

    public class GetBankByBankIdQueryHandler : IQueryHandler<GetBankByBankIdQuery, CustomerBank>
    {
        private readonly IRepository<CustomerBank> _repo;

        public GetBankByBankIdQueryHandler(IRepository<CustomerBank> repo)
        {
            _repo = repo;
        }
        public CustomerBank Handle(GetBankByBankIdQuery query)
        {
            return _repo.GetByID(query.BankId);
        }
    }
}
