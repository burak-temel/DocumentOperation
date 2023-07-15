using DocumentOperation.Data.Contexts;
using DocumentOperation.Data.Entities;

namespace DocumentOperation.API.Services
{
    public class DocumentService
    {
        private readonly AppDbContext _dbContext;

        public DocumentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void UploadDocument(Invoice document)
        {
            // Perform any necessary validation or preprocessing
            // Save the document and related entities to the database using the database context

            //var invoice = new Invoice
            //{
            //    InvoiceId = document.InvoiceHeader.InvoiceId,
            //    InvoiceHeader = new InvoiceHeader
            //    {
            //        SenderTitle = document.InvoiceHeader.SenderTitle,
            //        ReceiverTitle = document.InvoiceHeader.ReceiverTitle,
            //        Date = document.InvoiceHeader.Date,
            //        Email = document.InvoiceHeader.Email
            //    },
            //    InvoiceDetails = document.InvoiceLines.Select(line => new InvoiceDetail
            //    {
            //        Name = line.Name,
            //        Quantity = line.Quantity,
            //        UnitCode = line.UnitCode,
            //        UnitPrice = line.UnitPrice
            //    }).ToList()
            //};
            var invoice = new Invoice();
            _dbContext.Invoices.Add(invoice);
            _dbContext.SaveChanges();
        }

        public List<InvoiceHeader> GetDocumentHeaders()
        {
            return _dbContext.InvoiceHeaders.ToList();
        }

        public Invoice GetDocumentDetails(int documentId)
        {
            return _dbContext.Invoices
                .FirstOrDefault(invoice => invoice.Id == documentId);
        }
    }
}
