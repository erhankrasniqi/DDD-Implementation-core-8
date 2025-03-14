using Microsoft.EntityFrameworkCore;
using StarMart.Infrastructure.Contracts;
using StarMart.Infrastructure.Database;
using StarMart.SharedKernel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Save(CancellationToken cancellationToken = default)
        {
            SetDefaultProperties(_dbContext);
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void SetDefaultProperties(AppDbContext dbContext)
        {
            var modifiedItems = dbContext.ChangeTracker
                .Entries<IEntity<int>>()
                .Where(entity => entity.State == EntityState.Modified);

            var newItems = dbContext.ChangeTracker
                .Entries<IEntity<int>>()
                .Where(entity => entity.State == EntityState.Added);

            foreach (var item in modifiedItems)
            {
                item.Entity.SetModifiedOn(DateTime.UtcNow);
            }

            foreach (var item in newItems)
            {
                item.Entity.SetCreatedOn(DateTime.UtcNow);
            }
        }
    }
}
