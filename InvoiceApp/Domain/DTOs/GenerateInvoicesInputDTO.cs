using InvoiceApp.Domain.Enums;

namespace InvoiceApp.Domain.DTOs
{
    public class GenerateInvoicesInputDTO
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public EnInvoiceType Type { get; set; }
    }
}
