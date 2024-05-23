using InvoiceApp.Domain.Entities;
using InvoiceApp.Infrastructure;

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

        public List<Invoice> Execute()
        {
            List<Contract> contracts = _context.Contracts.ToList();
            Console.WriteLine(contracts);
            return new List<Invoice>();
        }
    }
}
