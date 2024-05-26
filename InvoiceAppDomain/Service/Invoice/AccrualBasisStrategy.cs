using InvoiceAppDomain.Entities;
using InvoiceAppDomain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InvoiceAppDomain.Service.Invoice
{
    public class AccrualBasisStrategy : IInvoiceGenerationStrategy
    {
        public List<InvoiceEntity> Generate(ContractEntity contract, int month, int year)
        {
            List<InvoiceEntity> invoices = new List<InvoiceEntity>();

            int period = 0;
            while (period <= contract.Periods)
            {
                DateTime date = contract.Date.AddMonths(period++);

                if (date.Month != month || date.Year != year)
                {
                    continue;
                }
                double amount = contract.Amount / contract.Periods;

                invoices.Add(new InvoiceEntity
                {
                    Date = date,
                    Amount = amount
                });
            }

            return invoices;
        }
    }
}
