namespace DocumentOperation.API.Models
{
    public class Document
    {
        public DocumentHeader InvoiceHeader { get; set; }
        public List<DocumentDetail> InvoiceLine { get; set; }
    }
}

