namespace Eventify.Domain.Enums;

/// <summary>
/// Represents the status of a waiting list entry for ticket availability.
/// </summary>
/// <remarks>
/// Users join the waiting list when tickets are unavailable. Upon availability, they receive
/// notification and the entry transitions to notified. If they purchase, it converts to the booking.
/// Entries expire if not converted within a specified timeframe.
/// </remarks>
public enum WaitingListStatus : byte
{
    /// <summary>The user is waiting for tickets to become available.</summary>
    Waiting = 0,
    /// <summary>The user has been notified that tickets are available.</summary>
    Notified = 1,
    /// <summary>The user has purchased tickets and the waiting list entry is converted to a booking.</summary>
    Converted = 2,
    /// <summary>The waiting list entry has expired and is no longer valid.</summary>
    Expired = 3
}