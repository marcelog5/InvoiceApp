using InvoiceAppDomain.Entities;

namespace InvoiceAppDomain.Service.Invoice
{
    public class CashBasisStrategy : IInvoiceGenerationStrategy
    {
        public List<InvoiceEntity> Generate(ContractEntity contract, int month, int year)
        {
            List<InvoiceEntity> invoices = new List<InvoiceEntity>();
            List<PaymentEntity> payments = contract.Payments.Where(x => x.ContractId == contract.Id).ToList();

            foreach (PaymentEntity payment in payments)
            {
                if (payment.Date.Month != month || payment.Date.Year != year)
                {
                    continue;
                }

                invoices.Add(new InvoiceEntity
                {
                    Date = payment.Date,
                    Amount = payment.Amount
                });
            }

            return invoices;
        }
    }
}
