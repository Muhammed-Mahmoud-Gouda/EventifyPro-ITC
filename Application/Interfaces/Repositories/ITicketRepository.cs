using Eventify.Domain.Entities;

namespace Eventify.Application.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<Ticket?> GetByQRCodeAsync(string qrCode);

    Task<IEnumerable<Ticket>> GetByBookingAsync(Guid bookingId);
}