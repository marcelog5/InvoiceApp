using InvoiceAppDomain.Entities;

namespace InvoiceAppDomain.Service.InvoiceServices
{
    public class CashBasisStrategy : IInvoiceGenerationStrategy
    {
        public List<Invoice> Generate(Contract contract, int month, int year)
        {
            List<Invoice> invoices = new List<Invoice>();
            List<Payment> payments = contract.Payments.Where(x => x.ContractId == contract.Id).ToList();

            foreach (Payment payment in payments)
            {
                if (payment.Date.Month != month || payment.Date.Year != year)
                {
                    continue;
                }

                invoices.Add(new Invoice
                {
                    Date = payment.Date,
                    Amount = payment.Amount
                });
            }

            return invoices;
        }
    }
}
