using AutoMapper;
using DocumentOperation.API.Models;
using DocumentOperation.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using DocumentOperation.ServiceContracts;
using Serilog;
using DocumentOperation.Core;

namespace DocumentOperation.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    [TypeFilter(typeof(FluentValidatorInterceptor))]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _documentService;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;

        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(InvoiceViewModel document)
        {
            var invoice = _mapper.Map<InvoiceDataModel>(document);
            await _documentService.UploadDocument(invoice);

            return Ok();
        }

        [HttpGet("headers")]
        public async Task<IActionResult> GetDocumentHeaders()
        {
            Log.Information("GetDocumentHeaders");

            var headers =await _documentService.GetDocumentHeaders();
            return Ok(headers);
        }
       
        [HttpGet("/details")]
        public async Task<IActionResult> GetDocumentDetails(string invoiceId)
        {
            var details = await _documentService.GetDocumentDetails(invoiceId);

            if (!details.Any())
            {
                return NotFound();
            }
            return Ok(details);
        }
    }
}
