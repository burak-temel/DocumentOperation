using DocumentOperation.API.Services;
using Microsoft.AspNetCore.Mvc;
using DocumentOperation.API.Models;
using DocumentOperation.API.Services;

namespace DocumentOperation.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public IActionResult UploadDocument(Document document)
        {
            // Call the DocumentService to process and save the document
            _documentService.ProcessAndSaveDocument(document);

            // Return a successful response
            return Ok();
        }

        [HttpGet("headers")]
        public IActionResult GetDocumentHeaders()
        {
            // Retrieve the list of document headers from the DocumentService
            var headers = _documentService.GetDocumentHeaders();

            // Return the document headers as a response
            return Ok(headers);
        }

        [HttpGet("{id}/details")]
        public IActionResult GetDocumentDetails(string id)
        {
            // Retrieve the document details from the DocumentService based on the provided ID
            var details = _documentService.GetDocumentDetails(id);

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
