using InvoiceApp.Domain.Entities;

namespace InvoiceApp.Infrastructure.Populate
{
    public static class InvoiceDBInitializer
    {
        public static void Initialize(InvoiceDBContext context)
        {
            context.Database.EnsureCreated(); // Ensures that the database is created

            // Check if there are any products already in the database.
            if (context.Contracts.Any())
            {
                return;   // Database has been seeded
            }

            // Seed the database with initial data
            Contract contract = new Contract
            {
                Description = "Prestação de serviços escolares",
                Amount = 6000,
                Periods = 12,
                Date = DateTime.Parse("2022-01-01T10:00:00"),
            };

            Payment payment = new Payment
            {
                Amount = 6000,
                Date = DateTime.Parse("2022-01-05T10:00:00"),
                Contract = contract
            };

            context.Payments.Add(payment);
            context.Contracts.Add(contract);

            context.SaveChanges();
        }
    }
}
