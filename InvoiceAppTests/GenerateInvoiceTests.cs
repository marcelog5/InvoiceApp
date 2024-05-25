using InvoiceApp.Service.InvoiceServices;
using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Entities;
using InvoiceAppDomain.Enums;
using Moq;

namespace InvoiceAppTests
{
    public class GenerateInvoiceTests
    {
        [Fact]
        public async Task ShouldGenerateInvoiceCash()
        {
            // Arrange
            Guid contractId = Guid.NewGuid();
            var contractResponse = new List<Contract>
            {
                new Contract
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
                },
            };

            var contractRepoMock = new Mock<IContractRepository>();
            contractRepoMock
                .Setup(repo => repo.GetAsync(It.IsAny<Func<IQueryable<Contract>, IQueryable<Contract>>>(), new CancellationToken()))
                .ReturnsAsync(contractResponse);

            var service = new GenerateInvoices(contractRepository: contractRepoMock.Object);
            var input = new GenerateInvoicesInputDTO
            {
                Month = 1,
                Year = 2022,
                Type = EnInvoiceType.Cash
            };

            // Act
            var result = await service.Execute(input);

            // Assert
            Assert.Equal(6000, result[0].Amount);
            Assert.Equal("2022-01-05", result[0].Date);
        }

        [Fact]
        public async Task ShouldGenerateInvoiceAccrual()
        {
            // Arrange
            Guid contractId = Guid.NewGuid();
            var contractResponse = new List<Contract>
            {
                new Contract
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
                },
            };

            var contractRepoMock = new Mock<IContractRepository>();
            contractRepoMock
                .Setup(repo => repo.GetAsync(It.IsAny<Func<IQueryable<Contract>, IQueryable<Contract>>>(), new CancellationToken()))
                .ReturnsAsync(contractResponse);

            var service = new GenerateInvoices(contractRepository: contractRepoMock.Object);
            var input = new GenerateInvoicesInputDTO
            {
                Month = 1,
                Year = 2022,
                Type = EnInvoiceType.Accrual
            };

            // Act
            var result = await service.Execute(input);

            // Assert
            Assert.Equal(500, result[0].Amount);
            Assert.Equal("2022-01-01", result[0].Date);
        }
    }
}
