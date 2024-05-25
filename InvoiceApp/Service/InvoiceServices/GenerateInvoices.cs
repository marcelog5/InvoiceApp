using InvoiceApp.Domain.DTOs;
using InvoiceApp.Domain.Entities;
using InvoiceApp.Domain.Enums;
using InvoiceApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Service.InvoiceServices
{
    public class GenerateInvoices
    {
        private readonly InvoiceDBContext _context;

        public GenerateInvoices
        (
            InvoiceDBContext context
        )
        {
            _context = context;
        }

        public List<GenerateInvoicesOutputDTO> Execute(GenerateInvoicesInputDTO input)
        {
            DbSet<Contract> contractQuery = _context.Set<Contract>();
            DbSet<Payment> paymentQuery = _context.Set<Payment>();
            List<GenerateInvoicesOutputDTO> output = new List<GenerateInvoicesOutputDTO>();

            List<Contract> contracts = contractQuery.ToList();
            
            foreach (Contract contract in contracts)
            {
                if (input.Type == EnInvoiceType.Cash)
                {
                    List<Payment> payments = paymentQuery.Where(x => x.ContractId == contract.Id).ToList();

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
