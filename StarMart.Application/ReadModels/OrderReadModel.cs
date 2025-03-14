namespace StarMart.Application.ReadModels
{
    public class OrderReadModel : OrderItemReadModel
    {
        public int CustomerId { get; set; }
        public string Customer { get; set; }
    }
}
