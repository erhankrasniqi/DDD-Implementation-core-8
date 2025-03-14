namespace StarMart.Application.ReadModels
{
    public class InvoiceItemReadModel
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
