using System.Threading.Tasks;
using EventifyPro.Domain.Entities;

namespace EventifyPro.DAL.Repositories.Interfaces
{
    public interface IWaitingListRepository : IGenericRepository<WaitingList>
    {
        Task<WaitingList?> GetNextWaitingAsync(int ticketTypeId);
        Task<WaitingList?> GetByUserAsync(string userId, int ticketTypeId);
        Task<bool> ExistsAsync(string userId, int ticketTypeId);
    }
}