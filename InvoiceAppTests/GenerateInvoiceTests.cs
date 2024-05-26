using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Entities;
using InvoiceAppDomain.Enums;
using InvoiceAppDomain.Service.Invoice;
using Moq;

namespace InvoiceAppTests
{
    // Integration tests
    public class GenerateInvoiceTests
    {
        [Fact]
        public async Task MustGenerateInvoiceCash()
        {
            // Arrange
            Guid contractId = Guid.NewGuid();
            var contractResponse = new List<ContractEntity>
            {
                new ContractEntity
                {
                    Id = contractId,
                    Description = "Prestação de serviços escolares",
                    Amount = 6000,
                    Periods = 12,
                    Date = DateTime.Parse("2022-01-01T10:00:00"),
                    Payments = new List<PaymentEntity>
                    {
                        new PaymentEntity
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
                .Setup(repo => repo.GetAsync(It.IsAny<Func<IQueryable<ContractEntity>, IQueryable<ContractEntity>>>(), new CancellationToken()))
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
        public async Task MustGenerateInvoiceAccrualByCSV()
        {
            // Arrange
            Guid contractId = Guid.NewGuid();
            var contractResponse = new List<ContractEntity>
            {
                new ContractEntity
                {
                    Id = contractId,
                    Description = "Prestação de serviços escolares",
                    Amount = 6000,
                    Periods = 12,
                    Date = DateTime.Parse("2022-01-01T10:00:00"),
                    Payments = new List<PaymentEntity>
                    {
                        new PaymentEntity
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
                .Setup(repo => repo.GetAsync(It.IsAny<Func<IQueryable<ContractEntity>, IQueryable<ContractEntity>>>(), new CancellationToken()))
                .ReturnsAsync(contractResponse);

            var service = new GenerateInvoices(contractRepository: contractRepoMock.Object);
            var input = new GenerateInvoicesInputDTO
            {
                Month = 1,
                Year = 2022,
                Type = EnInvoiceType.Accrual
            };

            // Act
            var result = await service.ExecuteCSV(input);

            // Assert
            Assert.Equal("2022-01-01;500", result);
        }

        [Fact]
        public async Task MustGenerateInvoiceAccrual()
        {
            // Arrange
            Guid contractId = Guid.NewGuid();
            var contractResponse = new List<ContractEntity>
            {
                new ContractEntity
                {
                    Id = contractId,
                    Description = "Prestação de serviços escolares",
                    Amount = 6000,
                    Periods = 12,
                    Date = DateTime.Parse("2022-01-01T10:00:00"),
                    Payments = new List<PaymentEntity>
                    {
                        new PaymentEntity
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
                .Setup(repo => repo.GetAsync(It.IsAny<Func<IQueryable<ContractEntity>, IQueryable<ContractEntity>>>(), new CancellationToken()))
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
