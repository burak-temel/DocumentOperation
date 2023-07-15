namespace DocumentOperation.API.Models
{
    public class DocumentDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitCode { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
