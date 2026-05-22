using Eventify.Domain.Entities;

namespace Eventify.Application.Interfaces.Repositories;

public interface IBookingItemRepository
{
    Task<IEnumerable<BookingItem>> GetByBookingAsync(Guid bookingId);

    Task<BookingItem?> GetWithTicketTypeAsync(Guid id);
}