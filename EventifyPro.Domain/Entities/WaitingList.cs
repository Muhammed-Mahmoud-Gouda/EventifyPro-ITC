using EventifyPro.Domain.Enums;
using System;
using System.Net.Sockets;

namespace EventifyPro.Domain.Entities
{
    public class WaitingList
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int TicketTypeId { get; set; }

        // FK to AspNetUsers
        public string UserId { get; set; } = string.Empty;

        public int QuantityWanted { get; set; }
        public WaitingListStatus Status { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public DateTime? NotifiedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        // Navigation Properties
        public Event Event { get; set; } = null!;
        public TicketType TicketType { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}