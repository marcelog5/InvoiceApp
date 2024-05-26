using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Service.InvoiceServices;

namespace InvoiceAppDomain.Entities
{
    public class Contract : BasicEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Periods { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Payment> Payments { get; set; }

        public double GetBalance()
        {
            double balance = Amount;
            foreach (Payment payment in Payments)
            {
                balance -= payment.Amount;
            }
            return balance;
        }

        public List<Invoice> GenerateInvoices(GenerateInvoicesInputDTO input)
        {
            IInvoiceGenerationStrategy invoiceGenerationStrategy = new InvoiceGenerationFactory().Create(input.Type);
            return invoiceGenerationStrategy.Generate(this, input.Month, input.Year);
        }
    }
}
