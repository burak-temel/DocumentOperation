using DocumentOperation.Core;
using DocumentOperation.Data.Contexts;
using DocumentOperation.Data.Entities;
using DocumentOperation.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;

namespace DocumentOperation.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _dbContext;

        public InvoiceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UploadDocument(InvoiceDataModel invoice)
        {
            await _dbContext.Invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<InvoiceHeaderDataModel>> GetDocumentHeaders()
        {
            return await _dbContext.InvoiceHeaders.ToListAsync();
        }

        public async Task<List<InvoiceDetailDataModel>> GetDocumentDetails(string invoiceId)
        {
            return await _dbContext.InvoiceDetails
                .Where(i => i.InvoiceId == invoiceId).ToListAsync();
        }

        public async Task<List<InvoiceDataModel>> GetUnprocessedDocuments()
        {
            var unprocessedDocument = await _dbContext.Invoices.Include(i => i.InvoiceLine).Include(i => i.InvoiceHeader)
                .Where(document => document.Status == (int)DocumentStatus.Unprocessed).OrderBy(i => i.Id).ToListAsync();

            return unprocessedDocument;
        }

        public async Task UpdateDocumentStatus(int id, DocumentStatus newStatus)
        {
            var document = await _dbContext.Invoices.FirstOrDefaultAsync(d => d.Id == id);

            if (document != null)
            {
                document.Status = (int)newStatus;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                Log.Information($"Document not found with invoice Id{id}.");
            }
        }
    }
}
