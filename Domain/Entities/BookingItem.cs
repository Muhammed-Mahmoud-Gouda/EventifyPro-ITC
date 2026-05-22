namespace Eventify.Domain.Entities;

public class BookingItem
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public Guid TicketTypeId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    // Navigation Properties
    public Booking Booking { get; set; }

    public TicketType TicketType { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}