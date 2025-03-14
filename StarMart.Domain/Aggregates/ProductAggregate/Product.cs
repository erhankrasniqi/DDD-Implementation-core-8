using StarMart.SharedKernel;

namespace StarMart.Domain.Aggregates.ProductAggregate
{
    public class Product : AggregateRoot<int>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public static Product Create(string name, decimal price)
        {
            Product product = new() { Name = name, Price = price };

            product.ValidateProduct();

            return product;
        }

        public void ChangeName(string name)
        {
            Name = name;

            ValidateProduct();
        }

        public void ChangePrice(decimal price)
        {
            Price = price;

            ValidateProduct();
        }

        private void ValidateProduct()
        {
            if (Name == null) ThrowDomainException("Product name is required.");

            if (Price < 1) ThrowDomainException("Product price is required.");
        }
    }
}
