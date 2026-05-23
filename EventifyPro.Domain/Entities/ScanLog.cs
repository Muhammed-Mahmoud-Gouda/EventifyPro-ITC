using EventifyPro.Domain.Enums;
using System;
using System.Net.Sockets;

namespace EventifyPro.Domain.Entities
{
    public class ScanLog
    {
        public int Id { get; set; }
        public int? TicketId { get; set; }
        public int EventId { get; set; }
        public int? ActualEventId { get; set; }

        // FK to AspNetUsers
        public string ScannedById { get; set; } = string.Empty;

        public DateTime ScannedAt { get; set; } = DateTime.UtcNow;
        public ScanResult Result { get; set; }

        public string? RawQRCode { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public Ticket? Ticket { get; set; }
        public Event ScanEvent { get; set; } = null!;
        public Event? ActualEvent { get; set; }
        public ApplicationUser Scanner { get; set; } = null!;
    }
}