namespace Eventify.Domain.Enums;

/// <summary>
/// Represents the status of a booking throughout its lifecycle.
/// </summary>
/// <remarks>
/// A booking transitions through states as it is created, confirmed, and potentially cancelled or refunded.
/// Each status indicates the booking's position in the complete lifecycle from purchase to completion.
/// </remarks>
public enum BookingStatus : byte
{
    /// <summary>Initial state when a booking is created but payment has not been confirmed.</summary>
    Pending = 0,
    /// <summary>The booking is confirmed with payment completed and tickets are reserved.</summary>
    Confirmed = 1,
    /// <summary>The booking has been cancelled by the user or system.</summary>
    Cancelled = 2,
    /// <summary>The booking has been refunded and is no longer valid.</summary>
    Refunded = 3
}
