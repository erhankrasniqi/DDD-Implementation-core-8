using StarMart.Domain.Aggregates.ProductAggregate;

namespace StarMart.Domain.Aggregates.CustomerAggregate
{
    public class ItemDetail
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public ItemDetail(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
