using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Service.Invoice;

namespace InvoiceAppDomain.Entities
{
    public class ContractEntity : BasicEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Periods { get; set; }
        public DateTime Date { get; set; }

        public virtual List<PaymentEntity> Payments { get; set; }

        public double GetBalance()
        {
            double balance = Amount;
            foreach (PaymentEntity payment in Payments)
            {
                balance -= payment.Amount;
            }
            return balance;
        }

        public List<InvoiceEntity> GenerateInvoices(GenerateInvoicesInputDTO input)
        {
            IInvoiceGenerationStrategy invoiceGenerationStrategy = new InvoiceGenerationFactory().Create(input.Type);
            return invoiceGenerationStrategy.Generate(this, input.Month, input.Year);
        }
    }
}
