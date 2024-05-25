namespace InvoiceAppDomain.Entities
{
    public class Contract : BasicEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Periods { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Payment> Payments { get; set; }
    }
}
