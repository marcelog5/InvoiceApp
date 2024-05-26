using InvoiceAppDomain.Entities;

namespace InvoiceAppDomain.Service.InvoiceServices
{
    public interface IInvoiceGenerationStrategy
    {
        public List<Invoice> Generate(Contract contract, int month, int year);
    }
}
