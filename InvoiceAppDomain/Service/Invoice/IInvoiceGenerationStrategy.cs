using InvoiceAppDomain.Entities;

namespace InvoiceAppDomain.Service.Invoice
{
    public interface IInvoiceGenerationStrategy
    {
        public List<InvoiceEntity> Generate(ContractEntity contract, int month, int year);
    }
}
