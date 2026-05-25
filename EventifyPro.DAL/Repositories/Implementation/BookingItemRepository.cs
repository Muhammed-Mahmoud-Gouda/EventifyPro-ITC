namespace EventifyPro.DAL.Repositories;

public class BookingItemRepository : GenericRepository<BookingItem>, IBookingItemRepository
{
    public BookingItemRepository(EventifyDbContext context) : base(context) { }

    public async Task<IReadOnlyList<BookingItem>> GetByBookingIdAsync(int bookingId, CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .Where(bi => bi.BookingId == bookingId)
            .Include(bi => bi.TicketType)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<BookingItem>> GetByTicketTypeIdAsync(int ticketTypeId, CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .Include(bi => bi.Booking)
                .ThenInclude(b => b.User)
            .Where(bi => bi.TicketTypeId == ticketTypeId
                      && bi.Booking.Status == BookingStatus.Confirmed)
            .OrderByDescending(bi => bi.Booking.BookingDate)
            .ToListAsync(cancellationToken);
}