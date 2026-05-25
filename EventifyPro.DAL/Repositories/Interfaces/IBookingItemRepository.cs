namespace EventifyPro.DAL.Repositories.Interfaces;

public interface IBookingItemRepository : IGenericRepository<BookingItem>
{
    /// <summary>
    /// All line items for a booking with their ticket type info.
    /// Hits IX_BookingItems_BookingId index.
    /// </summary>
    Task<IReadOnlyList<BookingItem>> GetByBookingIdAsync(int bookingId, CancellationToken cancellationToken = default);

    /// <summary>
    /// All booked items for a ticket type — for organizer analytics.
    /// Hits IX_BookingItems_TicketTypeId index.
    /// </summary>
    Task<IReadOnlyList<BookingItem>> GetByTicketTypeIdAsync(int ticketTypeId, CancellationToken cancellationToken = default);
}