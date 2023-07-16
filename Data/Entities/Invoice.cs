using DocumentOperation.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentOperation.Data.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public string InvoiceId { get; set; }
        public int Status { get; set; }
        public InvoiceHeader InvoiceHeader { get; set; }
        public List<InvoiceDetail> InvoiceLines { get; set; }
    }
}
