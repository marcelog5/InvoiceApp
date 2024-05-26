namespace InvoiceAppDomain.Entities
{
    public class InvoiceEntity : BasicEntity
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
