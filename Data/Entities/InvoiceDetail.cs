using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentOperation.Data.Entities
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitCode { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
