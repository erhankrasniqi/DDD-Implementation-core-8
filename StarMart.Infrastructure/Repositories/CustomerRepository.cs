using StarMart.Domain.Aggregates.CustomerAggregate;
using StarMart.Infrastructure.Contracts;
using StarMart.Infrastructure.Database;

namespace StarMart.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
            //
        }
    }
}
