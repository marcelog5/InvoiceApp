using InvoiceAppDomain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Requests
{
    public class GenerateInvoicesRequest
    {
        [FromBody]
        [Required]
        public int Month { get; set; }
        [FromBody]
        [Required]
        public int Year { get; set; }
        [FromBody]
        [Required]
        public EnInvoiceType Type { get; set; }
    }
}
