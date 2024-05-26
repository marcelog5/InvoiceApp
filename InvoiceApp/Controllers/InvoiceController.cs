using InvoiceApp.Requests;
using InvoiceApp.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IMediator _mediator;

        public InvoiceController
        (
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        // POST api/<InvoiceController>
        [HttpPost("GenerateInvoices")]
        public async Task<GenerateInvoicesResponse> Post([FromBody] GenerateInvoicesRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
