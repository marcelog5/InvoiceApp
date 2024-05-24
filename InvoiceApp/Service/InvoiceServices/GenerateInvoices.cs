using InvoiceApp.Domain.DTOs;
using InvoiceApp.Domain.Entities;
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
                List<Payment> payments = paymentQuery.Where(x => x.ContractId == contract.Id).ToList();

                foreach (Payment payment in payments)
                {
                    if (payment.Date.Month != input.Month || payment.Date.Year != input.Year)
                    {
                        continue;
                    }

                    output.Add(new GenerateInvoicesOutputDTO{
                        Date = payment.Date,
                        Amount = payment.Amount
                    });
                }
            }

            return output;
        }
    }
}
