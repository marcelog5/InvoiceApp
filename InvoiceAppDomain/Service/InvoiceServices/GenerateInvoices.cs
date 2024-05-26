using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAppDomain.Service.InvoiceServices
{
    public class GenerateInvoices
    {
        private readonly IContractRepository _contractRepository;

        public GenerateInvoices
        (
            IContractRepository contractRepository
        )
        {
            _contractRepository = contractRepository;
        }

        public async Task<List<GenerateInvoicesOutputDTO>> Execute(GenerateInvoicesInputDTO input)
        {
            List<GenerateInvoicesOutputDTO> output = new List<GenerateInvoicesOutputDTO>();

            Func<IQueryable<Contract>, IQueryable<Contract>> contractFilter = f => f.Where(x => x.IsActive).Include(x => x.Payments);
            List<Contract> contracts = (await _contractRepository.GetAsync(contractFilter)).ToList();

            foreach (Contract contract in contracts)
            {
                output.AddRange(contract.GenerateInvoices(input).
                    Select(x => new GenerateInvoicesOutputDTO
                    {
                        Amount = x.Amount,
                        Date = x.Date.ToString("yyyy-MM-dd")
                    }));
            }

            return output;
        }
    }
}
