using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Enums;

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

        public List<Invoice> GenerateInvoices(GenerateInvoicesInputDTO input)
        {
            List<Invoice> invoices = new List<Invoice>();

            if (input.Type == EnInvoiceType.Cash)
            {
                List<Payment> payments = Payments.Where(x => x.ContractId == Id).ToList();

                foreach (Payment payment in payments)
                {
                    if (payment.Date.Month != input.Month || payment.Date.Year != input.Year)
                    {
                        continue;
                    }

                    invoices.Add(new Invoice
                    {
                        Date = payment.Date,
                        Amount = payment.Amount
                    });
                }
            }

            if (input.Type == EnInvoiceType.Accrual)
            {
                int period = 0;
                while (period <= Periods)
                {
                    DateTime date = Date.AddMonths(period++);

                    if (date.Month != input.Month || date.Year != input.Year)
                    {
                        continue;
                    }
                    double amount = Amount / Periods;

                    invoices.Add(new Invoice
                    {
                        Date = date,
                        Amount = amount
                    });
                }
            }

            return invoices;
        }
    }
}
