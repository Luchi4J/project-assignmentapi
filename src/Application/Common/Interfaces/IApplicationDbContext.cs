using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAssignment.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
       
        DbSet<Domain.Entities.User> Users { get; set; }
        Task BeginTransactionAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync();
        void RollbackTransaction();
        string GetConnectionString();
    }
}
