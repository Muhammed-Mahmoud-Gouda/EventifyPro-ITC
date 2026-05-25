namespace EventifyPro.DAL.Repositories.Implementation;

/// <summary>
/// Repository implementation for TicketType entity operations.
/// Inherits from GenericRepository to provide standard CRUD operations.
/// </summary>
public class TicketTypeRepository : GenericRepository<TicketType>, ITicketTypeRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TicketTypeRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public TicketTypeRepository(EventifyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<TicketType>> GetTicketTypesByEventAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.EventId == eventId)
            .OrderBy(t => t.Price)
            .Include(t => t.Event)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TicketType>> GetAvailableTicketTypesAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.EventId == eventId && t.SoldQuantity < t.TotalQuantity)
            .OrderBy(t => t.Price)
            .Include(t => t.Event)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TicketType>> GetSoldOutTicketTypesAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.EventId == eventId && t.SoldQuantity >= t.TotalQuantity)
            .OrderBy(t => t.Price)
            .Include(t => t.Event)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TicketType>> GetActiveTicketTypesAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        return await _dbSet
            .Where(t => t.EventId == eventId
                && (!t.SaleStartDate.HasValue || t.SaleStartDate <= utcNow)
                && (!t.SaleEndDate.HasValue || t.SaleEndDate >= utcNow))
            .OrderBy(t => t.Price)
            .Include(t => t.Event)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetAvailableQuantityAsync(
        int ticketTypeId,
        CancellationToken cancellationToken = default)
    {
        var ticketType = await _dbSet
            .Where(t => t.Id == ticketTypeId)
            .Select(t => new { t.TotalQuantity, t.SoldQuantity })
            .FirstOrDefaultAsync(cancellationToken);

        if (ticketType == null)
        {
            return 0;
        }

        return Math.Max(0, ticketType.TotalQuantity - ticketType.SoldQuantity);
    }

    public async Task<bool> IsOnSaleAsync(
        int ticketTypeId,
        CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        return await _dbSet
            .Where(t => t.Id == ticketTypeId
                && (!t.SaleStartDate.HasValue || t.SaleStartDate <= utcNow)
                && (!t.SaleEndDate.HasValue || t.SaleEndDate >= utcNow))
            .AnyAsync(cancellationToken);
    }

    public async Task<TicketType?> GetCheapestTicketTypeAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.EventId == eventId)
            .OrderBy(t => t.Price)
            .Include(t => t.Event)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
