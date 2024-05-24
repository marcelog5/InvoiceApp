using InvoiceApp.Domain.DTOs;
using InvoiceApp.Domain.Entities;
using InvoiceApp.Domain.Enums;
using InvoiceApp.Infrastructure;
using InvoiceApp.Service.InvoiceServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace InvoiceAppTests
{
    public class GenerateInvoiceTests
    {
        private Mock<InvoiceDBContext> MockDbContext(IQueryable<Contract> contracts, IQueryable<Payment> payments)
        {
            var mockContractSet = new Mock<DbSet<Contract>>();
            mockContractSet.As<IQueryable<Contract>>().Setup(m => m.Provider).Returns(contracts.Provider);
            mockContractSet.As<IQueryable<Contract>>().Setup(m => m.Expression).Returns(contracts.Expression);
            mockContractSet.As<IQueryable<Contract>>().Setup(m => m.ElementType).Returns(contracts.ElementType);
            mockContractSet.As<IQueryable<Contract>>().Setup(m => m.GetEnumerator()).Returns(contracts.GetEnumerator());

            var mockPaymentSet = new Mock<DbSet<Payment>>();
            mockPaymentSet.As<IQueryable<Payment>>().Setup(m => m.Provider).Returns(payments.Provider);
            mockPaymentSet.As<IQueryable<Payment>>().Setup(m => m.Expression).Returns(payments.Expression);
            mockPaymentSet.As<IQueryable<Payment>>().Setup(m => m.ElementType).Returns(payments.ElementType);
            mockPaymentSet.As<IQueryable<Payment>>().Setup(m => m.GetEnumerator()).Returns(payments.GetEnumerator());

            var mockContext = new Mock<InvoiceDBContext>(new DbContextOptions<InvoiceDBContext>());
            mockContext.Setup(c => c.Set<Contract>()).Returns(mockContractSet.Object);
            mockContext.Setup(c => c.Set<Payment>()).Returns(mockPaymentSet.Object);

            return mockContext;
        }

        [Fact]
        public void ShouldGenerateInvoice()
        {
            // Arrange
            var contracts = new List<Contract>
            {
                new Contract
                {
                    Description = "Prestação de serviços escolares",
                    Amount = 6000,
                    Periods = 12,
                    Date = DateTime.Parse("2022-01-01T10:00:00"),
                },
            }.AsQueryable();

            var payments = new List<Payment>
            {
                new Payment
                {
                    Amount = 6000,
                    Date = DateTime.Parse("2022-01-05T10:00:00"),
                },
            }.AsQueryable();

            var mockContext = MockDbContext(contracts, payments);
            var service = new GenerateInvoices(mockContext.Object);
            var input = new GenerateInvoicesInputDTO
            {
                Month = 1,
                Year = 2022
            };

            // Act
            var result = service.Execute(input);

            // Assert
            Assert.Equal(6000, result[0].Amount);
            Assert.Equal("2022-01-05", result[0].Date.ToString("yyyy-MM-dd"));
        }
    }
}
