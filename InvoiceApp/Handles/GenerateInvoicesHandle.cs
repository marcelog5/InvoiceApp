using InvoiceApp.Requests;
using InvoiceApp.Responses;
using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Service.Invoice;

namespace InvoiceApp.Handles
{
    public class GenerateInvoicesHandle
    {
        private readonly IContractRepository _contractRepository;

        public GenerateInvoicesHandle
        (
            IContractRepository contractRepository
        )
        {
            _contractRepository = contractRepository;
        }

        public async Task<GenerateInvoicesResponse> Handle(GenerateInvoicesRequest request)
        {
            GenerateInvoicesInputDTO input = new GenerateInvoicesInputDTO
            {
                Month = request.Month,
                Year = request.Year,
                Type = request.Type
            };

            var generateInvoices = new GenerateInvoices(_contractRepository);
            var response = await generateInvoices.Execute(input);

            return new GenerateInvoicesResponse
            {
                Invoices = response
            };
        }
    }
}
