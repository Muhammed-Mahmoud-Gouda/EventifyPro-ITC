using Eventify.Domain.Enums;

namespace Eventify.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }

    public Guid AttendeeId { get; set; }

    public decimal TotalAmount { get; set; }

    public BookingStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public ApplicationUser Attendee { get; set; }

    public ICollection<BookingItem> BookingItems { get; set; } = new List<BookingItem>();
}