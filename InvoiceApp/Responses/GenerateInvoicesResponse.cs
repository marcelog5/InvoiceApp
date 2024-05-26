using InvoiceAppDomain.Data.DTOs;

namespace InvoiceApp.Responses
{
    public class GenerateInvoicesResponse
    {
        public List<GenerateInvoicesOutputDTO> Invoices { get; set; }
    }
}
