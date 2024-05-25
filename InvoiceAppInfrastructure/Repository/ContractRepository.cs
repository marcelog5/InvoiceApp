using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Entities;

namespace InvoiceAppInfrastructure.Repository
{
    public class ContractRepository : EntityFrameworkRepository<Contract, Guid>, IContractRepository
    {
        public ContractRepository(InvoiceDBContext dbContext) : base(dbContext) { }
    }
}
