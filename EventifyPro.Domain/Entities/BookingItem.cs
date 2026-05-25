namespace Eventify.Domain.Entities;

/// <summary>
/// Represents a line item in a booking, containing quantity and pricing for a specific ticket type.
/// Serves as a bridge between Booking and TicketType entities.
/// </summary>
/// <remarks>
/// BookingItem captures the quantity of tickets purchased of a specific type within a booking,
/// along with the unit price at the time of booking. This allows historical pricing tracking
/// even if the TicketType price changes later.
/// </remarks>
public class BookingItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the booking item.
    /// </summary>
    /// <value>The primary key.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the booking ID (foreign key).
    /// </summary>
    /// <value>The parent booking identifier.</value>
    public int BookingId { get; set; }

    /// <summary>
    /// Gets or sets the ticket type ID (foreign key).
    /// </summary>
    /// <value>The ticket type identifier.</value>
    public int TicketTypeId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of tickets of this type in the booking.
    /// </summary>
    /// <value>The number of tickets purchased.</value>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price per ticket at the time of booking.
    /// </summary>
    /// <value>The historical price per ticket.</value>
    public decimal UnitPrice { get; set; }

    // Navigation
    public Booking Booking { get; set; } = null!;
    public TicketType TicketType { get; set; } = null!;
}