using StarMart.Domain.Aggregates.ProductAggregate;
using StarMart.SharedKernel;

namespace StarMart.Domain.Aggregates.CustomerAggregate
{
    public class OrderItem : Entity<int>
    {
        public int OrderId { get; private set; }
        public Order Order { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public static OrderItem AddOrderItem(Order order, Product product, int quantity)
        {
            return new OrderItem
            {
                OrderId = order.Id,
                Order = order,
                ProductId = product.Id,
                Product = product,
                Quantity = quantity
            };
        }
    }
}
