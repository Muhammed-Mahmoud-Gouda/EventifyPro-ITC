namespace EventifyPro.DAL.Repositories.Implementation;

/// <summary>
/// Repository implementation for ScanLog entity operations.
/// Inherits from GenericRepository to provide standard CRUD operations.
/// </summary>
public class ScanLogRepository : GenericRepository<ScanLog>, IScanLogRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScanLogRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public ScanLogRepository(EventifyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<ScanLog>> GetScansByEventAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.EventId == eventId)
            .OrderByDescending(s => s.ScannedAt)
            .Include(s => s.Ticket)
            .Include(s => s.ScanEvent)
            .Include(s => s.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ScanLog>> GetScansByTicketAsync(
        int ticketId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.TicketId == ticketId)
            .OrderByDescending(s => s.ScannedAt)
            .Include(s => s.Ticket)
            .Include(s => s.ScanEvent)
            .Include(s => s.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ScanLog>> GetInvalidScansAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.EventId == eventId && s.Result != ScanResult.Valid)
            .OrderByDescending(s => s.ScannedAt)
            .Include(s => s.Ticket)
            .Include(s => s.ScanEvent)
            .Include(s => s.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ScanLog>> GetScansByDateRangeAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.ScannedAt >= startDate && s.ScannedAt <= endDate)
            .OrderByDescending(s => s.ScannedAt)
            .Include(s => s.Ticket)
            .Include(s => s.ScanEvent)
            .Include(s => s.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ScanLog>> GetScansByUserAsync(
        string scannedById,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(scannedById))
        {
            return [];
        }

        return await _dbSet
            .Where(s => s.ScannedById == scannedById)
            .OrderByDescending(s => s.ScannedAt)
            .Include(s => s.Ticket)
            .Include(s => s.ScanEvent)
            .Include(s => s.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ScanLog>> GetWrongEventScansAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.EventId == eventId && s.ActualEventId.HasValue && s.ActualEventId != eventId)
            .OrderByDescending(s => s.ScannedAt)
            .Include(s => s.Ticket)
            .Include(s => s.ScanEvent)
            .Include(s => s.ActualEvent)
            .Include(s => s.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
