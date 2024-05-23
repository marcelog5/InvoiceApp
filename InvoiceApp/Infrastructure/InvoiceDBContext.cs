using InvoiceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Infrastructure
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

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
    }
}
