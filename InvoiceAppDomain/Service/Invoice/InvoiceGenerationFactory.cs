using InvoiceAppDomain.Enums;

namespace InvoiceAppDomain.Service.Invoice
{
    public class InvoiceGenerationFactory
    {
        public IInvoiceGenerationStrategy Create(EnInvoiceType type)
        {
            return type switch
            {
                EnInvoiceType.Accrual => new AccrualBasisStrategy(),
                EnInvoiceType.Cash => new CashBasisStrategy(),
                _ => null,
            };
        }
    }
}
