using StarMart.Domain.Aggregates.ProductAggregate;
using StarMart.Infrastructure.Contracts;
using StarMart.Infrastructure.Database;

namespace StarMart.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
            //
        }
    }
}
