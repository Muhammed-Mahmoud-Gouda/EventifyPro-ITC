namespace EventifyPro.DAL.Repositories.Implementation;

/// <summary>
/// Repository implementation for WaitingList entity operations.
/// Inherits from GenericRepository to provide standard CRUD operations.
/// </summary>
public class WaitingListRepository : GenericRepository<WaitingList>, IWaitingListRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaitingListRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public WaitingListRepository(EventifyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<WaitingList>> GetUserWaitingListsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return [];
        }

        return await _dbSet
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.JoinedAt)
            .Include(w => w.Event)
            .Include(w => w.TicketType)
            .Include(w => w.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WaitingList>> GetTicketTypeWaitingListAsync(
        int ticketTypeId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(w => w.TicketTypeId == ticketTypeId)
            .OrderBy(w => w.JoinedAt)
            .Include(w => w.Event)
            .Include(w => w.TicketType)
            .Include(w => w.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WaitingList>> GetNotifiedWaitingListsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(w => w.NotifiedAt.HasValue)
            .OrderByDescending(w => w.NotifiedAt)
            .Include(w => w.Event)
            .Include(w => w.TicketType)
            .Include(w => w.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WaitingList>> GetExpiredWaitingListsAsync(
        CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        return await _dbSet
            .Where(w => w.ExpiresAt.HasValue && w.ExpiresAt <= utcNow)
            .OrderByDescending(w => w.ExpiresAt)
            .Include(w => w.Event)
            .Include(w => w.TicketType)
            .Include(w => w.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WaitingList>> GetActiveWaitingListsForEventAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        return await _dbSet
            .Where(w => w.EventId == eventId && (!w.ExpiresAt.HasValue || w.ExpiresAt > utcNow))
            .OrderBy(w => w.JoinedAt)
            .Include(w => w.Event)
            .Include(w => w.TicketType)
            .Include(w => w.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalRequestedTicketsAsync(
        int ticketTypeId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(w => w.TicketTypeId == ticketTypeId && w.Status == WaitingListStatus.Waiting)
            .SumAsync(w => w.QuantityWanted, cancellationToken);
    }

    public async Task<bool> IsUserOnWaitingListAsync(
        string userId,
        int ticketTypeId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return false;
        }

        return await _dbSet
            .AnyAsync(w => w.UserId == userId && w.TicketTypeId == ticketTypeId, cancellationToken);
    }
}
