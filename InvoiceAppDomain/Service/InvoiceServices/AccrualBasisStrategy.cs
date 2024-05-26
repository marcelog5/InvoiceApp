using InvoiceAppDomain.Entities;
using InvoiceAppDomain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InvoiceAppDomain.Service.InvoiceServices
{
    public class AccrualBasisStrategy : IInvoiceGenerationStrategy
    {
        public List<Invoice> Generate(Contract contract, int month, int year)
        {
            List<Invoice> invoices = new List<Invoice>();

            int period = 0;
            while (period <= contract.Periods)
            {
                DateTime date = contract.Date.AddMonths(period++);

                if (date.Month != month || date.Year != year)
                {
                    continue;
                }
                double amount = contract.Amount / contract.Periods;

                invoices.Add(new Invoice
                {
                    Date = date,
                    Amount = amount
                });
            }

            return invoices;
        }
    }
}
