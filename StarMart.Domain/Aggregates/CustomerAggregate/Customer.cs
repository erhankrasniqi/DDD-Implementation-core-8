using StarMart.SharedKernel;
using System.Collections.Generic;
using System.Linq;

namespace StarMart.Domain.Aggregates.CustomerAggregate
{
    public class Customer : AggregateRoot<int>
    {
        public string FirstName { get; private set; }
        public string Lastname { get; private set; }
        public string PostalCode { get; private set; }
        public string Address { get; private set; }
        public ICollection<Order> Orders { get; set; }

        public static Customer Create(string firstName, string lastName, string postalCode, string address)
        {
            Customer customer = new()
            {
                FirstName = firstName,
                Lastname = lastName,
                PostalCode = postalCode,
                Address = address
            };

            customer.ValidateCustomer();

            return customer;
        }

        public void ChangePersonalInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            Lastname = lastName;

            ValidateCustomer();
        }

        public void ChangeAddressDetails(string postalCode, string address)
        {
            PostalCode = postalCode;
            Address = address;

            ValidateCustomer();
        }

        public Order SubmitOrder(IList<ItemDetail> items)
        {
            Order order = Order.Create(this);

            foreach (ItemDetail item in items)
            {
                order.AddItem(item.Product, item.Quantity);
            }

            ValidateOrder(order);

            order.Submit();

            return order;
        }

        private void ValidateCustomer()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(Lastname)) ThrowDomainException("First name and last name are required.");

            if (string.IsNullOrEmpty(PostalCode)) ThrowDomainException("Postal code is required. Order cannot be submitted without postal code.");

            if (string.IsNullOrEmpty(Address)) ThrowDomainException("Address is required. Order cannot be submitted without address.");
        }

        private void ValidateOrder(Order order)
        {
            if (order.Customer == null || order.CustomerId < 1) ThrowDomainException("Only valid customer can submit order.");

            if (!order.OrderItems.Any()) ThrowDomainException("At least one item is required for submitting order.");

            foreach (OrderItem item in order.OrderItems)
            {
                if (item.Product == null || item.ProductId < 1) ThrowDomainException("Only valid product can be added in order items.");

                if (item.Quantity < 0) ThrowDomainException($"Quantity for {item.Product.Name} must be greater than 0.");
            }
        }
    }
}
