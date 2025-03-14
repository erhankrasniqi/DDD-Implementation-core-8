using StarMart.Domain.Aggregates.ProductAggregate;
using StarMart.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarMart.Domain.Aggregates.CustomerAggregate
{
    public class Order : Entity<int>
    {
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];

        public static Order Create(Customer customer)
        {
            return new Order { CustomerId = customer.Id, Customer = customer };
        }

        public void AddItem(Product product, int quantity)
        {
            OrderItems.Add(OrderItem.AddOrderItem(this, product, quantity));
        }

        public void Submit()
        {
            OrderDate = DateTime.UtcNow;
            TotalPrice = OrderItems.Sum(x => x.Product.Price * x.Quantity);
        }
    }
}
