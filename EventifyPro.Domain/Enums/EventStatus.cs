namespace Eventify.Domain.Enums;

/// <summary>
/// Represents the status of an event from creation through completion or cancellation.
/// </summary>
/// <remarks>
/// Events start as drafts for organizer preparation, transition to published for public availability,
/// and eventually reach a terminal state of completion or cancellation. Cancelled events are not
/// available for new bookings.
/// </remarks>
public enum EventStatus : byte
{
    /// <summary>The event is being prepared and is not yet visible to attendees.</summary>
    Draft = 0,
    /// <summary>The event is published and available for public booking.</summary>
    Published = 1,
    /// <summary>The event has been cancelled and no new bookings are accepted.</summary>
    Cancelled = 2,
    /// <summary>The event has occurred and is marked as completed.</summary>
    Completed = 3
}