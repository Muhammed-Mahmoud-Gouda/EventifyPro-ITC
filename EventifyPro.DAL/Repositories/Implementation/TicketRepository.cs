namespace EventifyPro.DAL.Repositories.Implementation;

/// <summary>
/// Repository implementation for Ticket entity operations.
/// Inherits from GenericRepository to provide standard CRUD operations.
/// </summary>
public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TicketRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public TicketRepository(EventifyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Ticket?> GetTicketByQRCodeAsync(
        string qrCode,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(qrCode))
        {
            return null;
        }

        return await _dbSet
            .Where(t => t.QRCode == qrCode)
            .Include(t => t.Event)
            .Include(t => t.Booking)
            .Include(t => t.TicketType)
            .Include(t => t.Scanner)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Ticket>> GetTicketsByBookingAsync(
        int bookingId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.BookingId == bookingId)
            .OrderBy(t => t.CreatedAt)
            .Include(t => t.Event)
            .Include(t => t.Booking)
            .Include(t => t.TicketType)
            .Include(t => t.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Ticket>> GetTicketsByEventAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.EventId == eventId)
            .OrderBy(t => t.CreatedAt)
            .Include(t => t.Event)
            .Include(t => t.Booking)
            .Include(t => t.TicketType)
            .Include(t => t.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Ticket>> GetUnusedTicketsAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.EventId == eventId && !t.IsUsed)
            .OrderBy(t => t.CreatedAt)
            .Include(t => t.Event)
            .Include(t => t.Booking)
            .Include(t => t.TicketType)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Ticket>> GetUsedTicketsAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.EventId == eventId && t.IsUsed)
            .OrderByDescending(t => t.UsedAt)
            .Include(t => t.Event)
            .Include(t => t.Booking)
            .Include(t => t.TicketType)
            .Include(t => t.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Ticket>> GetTicketsByTypeAsync(
        int ticketTypeId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.TicketTypeId == ticketTypeId)
            .OrderBy(t => t.CreatedAt)
            .Include(t => t.Event)
            .Include(t => t.Booking)
            .Include(t => t.TicketType)
            .Include(t => t.Scanner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUsedTicketsCountAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.EventId == eventId && t.IsUsed)
            .CountAsync(cancellationToken);
    }

    public async Task<double> GetAttendanceRateAsync(
        int eventId,
        CancellationToken cancellationToken = default)
    {
        var totalTickets = await _dbSet
            .Where(t => t.EventId == eventId)
            .CountAsync(cancellationToken);

        if (totalTickets == 0)
        {
            return 0;
        }

        var usedTickets = await _dbSet
            .Where(t => t.EventId == eventId && t.IsUsed)
            .CountAsync(cancellationToken);

        return (usedTickets / (double)totalTickets) * 100;
    }
}
