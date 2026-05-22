namespace Eventify.Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; }

    public Guid BookingItemId { get; set; }

    public string QRCode { get; set; }

    public bool IsUsed { get; set; } = false;

    public DateTime? UsedAt { get; set; }

    public Guid? ScannedById { get; set; }

    // Navigation Properties
    public BookingItem BookingItem { get; set; }

    public ApplicationUser ScannedBy { get; set; }
}