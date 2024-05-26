using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAppDomain.Service.Invoice
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

        private async Task<List<GenerateInvoicesOutputDTO>> GenerateInvoice(GenerateInvoicesInputDTO input)
        {
            List<GenerateInvoicesOutputDTO> output = new List<GenerateInvoicesOutputDTO>();

            Func<IQueryable<ContractEntity>, IQueryable<ContractEntity>> contractFilter = f => f.Where(x => x.IsActive).Include(x => x.Payments);
            List<ContractEntity> contracts = (await _contractRepository.GetAsync(contractFilter)).ToList();

            foreach (ContractEntity contract in contracts)
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

        public async Task<List<GenerateInvoicesOutputDTO>> Execute(GenerateInvoicesInputDTO input)
        {
            return await GenerateInvoice(input);
        }

        public async Task<string> ExecuteCSV(GenerateInvoicesInputDTO input)
        {
            var generatedInvoices = await GenerateInvoice(input);

            var lines = new List<string>();
            foreach (var data in generatedInvoices)
            {
                var line = new List<string>
                {
                    data.Date,
                    data.Amount.ToString()
                };
                lines.Add(string.Join(";", line));
            }
            return string.Join("\n", lines);
        }
    }
}
