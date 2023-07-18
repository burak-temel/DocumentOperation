using DocumentOperation.Core;
using DocumentOperation.Data.Entities;

namespace DocumentOperation.ServiceContracts
{
    public interface IInvoiceService
    {
        public Task UpdateDocumentStatus(int id, DocumentStatus newStatus);
        public Task<List<InvoiceDataModel>> GetUnprocessedDocuments();
        public Task<List<InvoiceDetailDataModel>> GetDocumentDetails(string invoiceId);
        public Task<List<InvoiceHeaderDataModel>> GetDocumentHeaders();
        public Task UploadDocument(InvoiceDataModel invoice);
    }
}
