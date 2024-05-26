namespace InvoiceAppDomain.Entities
{
    public class Invoice : BasicEntity
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
