using Eventify.Domain.Entities;

namespace Eventify.Application.Interfaces.Repositories;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetByAttendeeAsync(Guid attendeeId);

    Task<Booking?> GetWithItemsAsync(Guid bookingId);

    Task<IEnumerable<Booking>> GetPendingAsync();
}