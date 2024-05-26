using InvoiceAppDomain.Data.DTOs;
using InvoiceAppDomain.Data.Repository;
using InvoiceAppDomain.Enums;
using InvoiceAppDomain.Service.InvoiceServices;
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

        // GET: api/<InvoiceController>
        [HttpGet]
        public async Task<List<GenerateInvoicesOutputDTO>> Get()
        {
            var generateInvoices = new GenerateInvoices(_contractRepository);
            var response = await generateInvoices.Execute(new GenerateInvoicesInputDTO
            {
                Month = 1,
                Year = 2022,
                Type = EnInvoiceType.Cash
            });
            return response;
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
