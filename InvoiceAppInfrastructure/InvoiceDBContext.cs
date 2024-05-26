using InvoiceAppDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAppInfrastructure
{
    public class InvoiceDBContext : DbContext
    {
        public InvoiceDBContext(DbContextOptions<InvoiceDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<InvoiceEntity> Invoices { get; set; }
        public virtual DbSet<ContractEntity> Contracts { get; set; }
        public virtual DbSet<PaymentEntity> Payments { get; set; }
    }
}
