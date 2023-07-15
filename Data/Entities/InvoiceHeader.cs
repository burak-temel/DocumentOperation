using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentOperation.Data.Entities
{
    public class InvoiceHeader
    {
        public int Id { get; set; }
        public string InvoiceId { get; set; }
        public string SenderTitle { get; set; }
        public string ReceiverTitle { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
    }
}
