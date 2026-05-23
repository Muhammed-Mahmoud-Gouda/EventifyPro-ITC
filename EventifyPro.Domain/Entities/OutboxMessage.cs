using System;

namespace EventifyPro.Domain.Entities
{
    public class OutboxMessage
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ScheduledFor { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int RetryCount { get; set; } = 0;
        public string? LastError { get; set; }
    }
}