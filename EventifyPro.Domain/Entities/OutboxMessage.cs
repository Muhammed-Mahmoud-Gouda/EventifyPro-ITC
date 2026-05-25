namespace Eventify.Domain.Entities;

/// <summary>
/// Represents an outbox message for reliable event publishing using the Outbox pattern.
/// Supports retry logic, scheduling, and error tracking for event sourcing.
/// </summary>
/// <remarks>
/// The Outbox pattern ensures reliable event publishing by storing events in the database
/// as part of the same transaction that modifies domain state. A separate process polls this
/// table and publishes events to message queues, supporting eventual consistency patterns.
/// </remarks>
public class OutboxMessage
{
    /// <summary>
    /// Gets or sets the unique identifier for the outbox message.
    /// </summary>
    /// <value>The primary key.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of event contained in this message.
    /// </summary>
    /// <value>The event type name.</value>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the serialized event payload.
    /// </summary>
    /// <value>The JSON-serialized event data.</value>
    public string Payload { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the scheduled time for event publishing.
    /// </summary>
    /// <value>The scheduled publication time, or null for immediate processing.</value>
    public DateTime? ScheduledFor { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the event was successfully published.
    /// </summary>
    /// <value>The publication timestamp, or null if not yet processed.</value>
    public DateTime? ProcessedAt { get; set; }

    /// <summary>
    /// Gets or sets the number of times this message has been retried.
    /// </summary>
    /// <value>The retry count.</value>
    public int RetryCount { get; set; } = 0;

    /// <summary>
    /// Gets or sets the last error message encountered during processing.
    /// </summary>
    /// <value>The error message, or null if no errors have occurred.</value>
    public string? LastError { get; set; }
}