using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace EventifyPro.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}