using StarMart.Domain.Aggregates.CustomerAggregate;

namespace StarMart.Infrastructure.Contracts
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        //
    }
}
