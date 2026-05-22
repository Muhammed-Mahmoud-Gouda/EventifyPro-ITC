using Eventify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TotalAmount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(x => x.Status)
               .IsRequired();

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.HasOne(x => x.Attendee)
               .WithMany(x => x.Bookings)
               .HasForeignKey(x => x.AttendeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.BookingItems)
               .WithOne(x => x.Booking)
               .HasForeignKey(x => x.BookingId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}