using Eventify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Infrastructure.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.QRCode)
               .IsRequired()
               .HasMaxLength(500);

        builder.HasIndex(x => x.QRCode)
               .IsUnique();

        builder.Property(x => x.IsUsed)
               .HasDefaultValue(false);

        builder.HasOne(x => x.BookingItem)
               .WithMany(x => x.Tickets)
               .HasForeignKey(x => x.BookingItemId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ScannedBy)
               .WithMany()
               .HasForeignKey(x => x.ScannedById)
               .OnDelete(DeleteBehavior.Restrict);
    }
}