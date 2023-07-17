namespace DocumentOperation.API.Models
{
    public class InvoiceViewModel
    {
        public InvoiceHeaderViewModel InvoiceHeader { get; set; }
        public List<InvoiceDetailViewModel> InvoiceLine { get; set; }
    }
}

