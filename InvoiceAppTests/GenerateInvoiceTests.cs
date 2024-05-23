using InvoiceApp.Domain.Entities;
using InvoiceApp.Infrastructure;
using InvoiceApp.Service.InvoiceServices;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace InvoiceAppTests
{
    public class GenerateInvoiceTests
    {
        [Fact]
        public void ShouldGenerateInvoice()
        {
            // Arrange
            var contracts = new List<Contract>
            {
                new Contract {  },
                new Contract {  }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Contract>>();
            mockSet.As<IQueryable<Contract>>().Setup(m => m.Provider).Returns(contracts.Provider);
            mockSet.As<IQueryable<Contract>>().Setup(m => m.Expression).Returns(contracts.Expression);
            mockSet.As<IQueryable<Contract>>().Setup(m => m.ElementType).Returns(contracts.ElementType);
            mockSet.As<IQueryable<Contract>>().Setup(m => m.GetEnumerator()).Returns(contracts.GetEnumerator());

            var mockContext = new Mock<InvoiceDBContext>(new DbContextOptions<InvoiceDBContext>());
            mockContext.Setup(c => c.Contracts).Returns(mockSet.Object);

            var generateInvoices = new GenerateInvoices(mockContext.Object);

            // Act
            List<Invoice> invoice = generateInvoices.Execute();

            // Assert
            Assert.Empty(invoice);
        }
    }
}