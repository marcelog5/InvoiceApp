namespace InvoiceAppDomain.Entities
{
    public class PaymentEntity : BasicEntity
    {
        public Guid ContractId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public ContractEntity Contract { get; set; }
    }
}
