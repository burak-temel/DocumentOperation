﻿namespace DocumentOperation.Data.Entities
{
    public class InvoiceDetailDataModel
    {
        public int Id { get; set; }
        public int InvoiceDetailId { get; set; }
        public string InvoiceId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitCode { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
