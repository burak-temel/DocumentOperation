using AutoMapper;
using DocumentOperation.API.Models;
using DocumentOperation.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using DocumentOperation.ServiceContracts;
using Serilog;

namespace DocumentOperation.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;

        public DocumentController(IDocumentService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;

        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(InvoiceViewModel document)
        {
            Log.Information("Scheduling job");
            var invoice = _mapper.Map<Invoice>(document);

            // Call the DocumentService to process and save the document
            await _documentService.UploadDocument(invoice);


            // Return a successful response
            return Ok();
        }

        [HttpGet("headers")]
        public async Task<IActionResult> GetDocumentHeaders()
        {
            Log.Information("Scheduling job");

            // Retrieve the list of document headers from the DocumentService
            var headers =await _documentService.GetDocumentHeaders();
            // Return the document headers as a response
            return Ok(headers);
        }
       
        [HttpGet("/details")]
        public async Task<IActionResult> GetDocumentDetails(string invoiceId)
        {
            // Retrieve the document details from the DocumentService based on the provided ID
            var details = await _documentService.GetDocumentDetails(invoiceId);

            if (details == null)
            {
                // Return a 404 Not Found response if the document details are not found
                return NotFound();
            }

            // Return the document details as a response
            return Ok(details);
        }
    }
}
