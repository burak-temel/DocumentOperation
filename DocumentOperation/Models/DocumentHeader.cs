using DocumentOperation.Data.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DocumentOperation.API.Models
{
    public class DocumentHeader
    {
        public string InvoiceId { get; set; }
        public string SenderTitle { get; set; }
        public string ReceiverTitle { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
    }
}
