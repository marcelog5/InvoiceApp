using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Entities;
using InvoiceAppDomain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Service.InvoiceServices
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
                if (input.Type == EnInvoiceType.Cash)
                {
                    List<Payment> payments = contract.Payments.Where(x => x.ContractId == contract.Id).ToList();

                    foreach (Payment payment in payments)
                    {
                        if (payment.Date.Month != input.Month || payment.Date.Year != input.Year)
                        {
                            continue;
                        }

                        output.Add(new GenerateInvoicesOutputDTO{
                            Date = payment.Date.ToString("yyyy-MM-dd"),
                            Amount = payment.Amount
                        });
                    }
                }

                if (input.Type == EnInvoiceType.Accrual)
                {
                    int period = 0;
                    while (period <= contract.Periods)
                    {
                        DateTime date = contract.Date.AddMonths(period++);
                        
                        if (date.Month != input.Month || date.Year != input.Year)
                        {
                            continue;
                        }

                        double amount = contract.Amount / contract.Periods;

                        output.Add(new GenerateInvoicesOutputDTO
                        {
                            Date = date.ToString("yyyy-MM-dd"),
                            Amount = amount
                        });
                    }
                }
            }

            return output;
        }
    }
}
