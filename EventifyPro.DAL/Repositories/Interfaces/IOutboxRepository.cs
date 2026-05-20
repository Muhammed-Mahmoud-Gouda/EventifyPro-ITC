using System.Collections.Generic;
using System.Threading.Tasks;
using EventifyPro.Domain.Entities;

namespace EventifyPro.DAL.Repositories.Interfaces
{
    public interface IOutboxRepository : IGenericRepository<OutboxMessage>
    {
        Task<IEnumerable<OutboxMessage>> GetPendingAsync(int maxRetry, int batchSize);
        Task MarkProcessedAsync(int messageId);
        Task IncrementRetryAsync(int messageId, string error);
    }
}