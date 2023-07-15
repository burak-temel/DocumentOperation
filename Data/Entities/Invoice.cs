using DocumentOperation.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentOperation.Data.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public InvoiceHeader InvoiceHeader { get; set; }
        public List<InvoiceDetail> InvoiceLine { get; set; }
    }
}
