namespace InvoiceAppDomain.Entities
{
    public class Payment : BasicEntity
    {
        public Guid ContractId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public Contract Contract { get; set; }
    }
}
