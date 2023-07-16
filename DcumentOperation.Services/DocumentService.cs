﻿using DocumentOperation.Core;
using DocumentOperation.Data.Contexts;
using DocumentOperation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentOperation.Services
{
    public class DocumentService
    {
        private readonly AppDbContext _dbContext;

        public DocumentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UploadDocument(Invoice invoice)
        {
            // Perform any necessary validation or preprocessing
            // Save the document and related entities to the database using the database context

            await _dbContext.Invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<InvoiceHeader>> GetDocumentHeaders()
        {
            return await _dbContext.InvoiceHeaders.ToListAsync();
        }

        public async Task<List<InvoiceDetail>> GetDocumentDetails(string invoiceId)
        {
            return await _dbContext.InvoiceDetails
                .Where(i => i.InvoiceId == invoiceId).ToListAsync();
        }

        public async Task<List<Invoice>> GetUnprocessedDocuments()
        {
            // Query the database to retrieve unprocessed documents
            var unprocessedDocument = await _dbContext.Invoices.Include(i=> i.InvoiceLines)
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
                //throw new NotFoundException("Document not found."); // Custom exception for document not found
            }
        }
    }
}
