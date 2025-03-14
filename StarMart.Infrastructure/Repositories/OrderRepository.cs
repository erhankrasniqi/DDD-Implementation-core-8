using StarMart.Domain.Aggregates.CustomerAggregate;
using StarMart.Infrastructure.Contracts;
using StarMart.Infrastructure.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StarMart.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            //
        }

        public async Task CreateOrder(Customer customer, IList<ItemDetail> items, CancellationToken cancellationToken = default)
        {
            if (customer == null) throw new ApplicationException("Customer not found.");

            if (items == null || !items.Any()) throw new ApplicationException("Invalid order.");

            Order order = customer.SubmitOrder(items);

            await Add(order, cancellationToken).ConfigureAwait(false);
        }
    }
}
