using InvoiceApp.Handles;
using InvoiceApp.Requests;
using InvoiceApp.Responses;
using InvoiceAppDomain.Data.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;

        public InvoiceController
        (
            IContractRepository contractRepository
        )
        {
            _contractRepository = contractRepository;
        }

        // POST api/<InvoiceController>
        [HttpPost("GenerateInvoices")]
        public async Task<GenerateInvoicesResponse> Post([FromBody] GenerateInvoicesRequest request)
        {
            GenerateInvoicesHandle handler = new GenerateInvoicesHandle(_contractRepository);
            GenerateInvoicesResponse response = await handler.Handle(request);

            return response;
        }
    }
}
