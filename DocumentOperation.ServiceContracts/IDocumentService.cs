using DocumentOperation.Core;
using DocumentOperation.Data.Entities;

namespace DocumentOperation.ServiceContracts
{
    public interface IDocumentService
    {
        public Task UpdateDocumentStatus(int id, DocumentStatus newStatus);
        public Task<List<Invoice>> GetUnprocessedDocuments();
        public Task<List<InvoiceDetail>> GetDocumentDetails(string invoiceId);
        public Task<List<InvoiceHeader>> GetDocumentHeaders();
        public Task UploadDocument(Invoice invoice);
    }
}
