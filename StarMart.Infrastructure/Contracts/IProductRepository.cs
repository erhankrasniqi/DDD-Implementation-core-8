using StarMart.Domain.Aggregates.ProductAggregate;

namespace StarMart.Infrastructure.Contracts
{
    public interface IProductRepository : IRepository<Product, int>
    {
        //
    }
}
