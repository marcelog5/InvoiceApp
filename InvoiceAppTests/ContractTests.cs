using InvoiceApp.Service.InvoiceServices;
using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Entities;
using InvoiceAppDomain.Enums;
using Moq;

namespace InvoiceAppTests
{
    // Unit tests
    public class ContractTests
    {
        [Fact]
        public async Task MustGenerateInvoiceForAContract()
        {
            // Arrange
            Guid contractId = Guid.NewGuid();
            var contractDummy = new Contract
            {
                Id = contractId,
                Description = "Prestação de serviços escolares",
                Amount = 6000,
                Periods = 12,
                Date = DateTime.Parse("2022-01-01T10:00:00"),
                Payments = new List<Payment>
                    {
                        new Payment
                        {
                            ContractId = contractId,
                            Amount = 6000,
                            Date = DateTime.Parse("2022-01-05T10:00:00"),
                        },
                    },
            };

            var input = new GenerateInvoicesInputDTO
            {
                Month = 1,
                Year = 2022,
                Type = EnInvoiceType.Accrual
            };

            // Act
            var result = contractDummy.GenerateInvoices(input);

            // Assert
            Assert.Equal(500, result[0].Amount);
            Assert.Equal(DateTime.Parse("2022-01-01T10:00:00"), result[0].Date);
        }
    }
}
