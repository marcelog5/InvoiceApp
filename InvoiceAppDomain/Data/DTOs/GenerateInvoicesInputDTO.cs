using InvoiceAppDomain.Enums;

namespace InvoiceAppDomain.Data.DTOs
{
    public class GenerateInvoicesInputDTO
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public EnInvoiceType Type { get; set; }
    }
}
