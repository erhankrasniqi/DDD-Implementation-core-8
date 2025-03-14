using StarMart.Domain.Aggregates.CustomerAggregate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Infrastructure.Contracts
{
    public interface IOrderRepository : IRepository<Order, int>
    {
        Task CreateOrder(Customer customer, IList<ItemDetail> items, CancellationToken cancellationToken = default);
    }
}
