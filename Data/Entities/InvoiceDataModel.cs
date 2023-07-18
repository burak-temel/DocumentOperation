using DocumentOperation.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentOperation.Data.Entities
{
    public class InvoiceDataModel
    {
        public int Id { get; set; }
        public string InvoiceId { get; set; }
        public int Status { get; set; } = 1;
        public InvoiceHeaderDataModel InvoiceHeader { get; set; }
        public List<InvoiceDetailDataModel> InvoiceLine { get; set; }
    }
}
