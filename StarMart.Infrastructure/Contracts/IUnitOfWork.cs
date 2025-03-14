using System.Threading;
using System.Threading.Tasks;

namespace StarMart.Infrastructure.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> Save(CancellationToken cancellationToken = default);
    }
}
