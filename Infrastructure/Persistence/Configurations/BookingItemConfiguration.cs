using Eventify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Infrastructure.Persistence.Configurations;

public class BookingItemConfiguration : IEntityTypeConfiguration<BookingItem>
{
    public void Configure(EntityTypeBuilder<BookingItem> builder)
    {
        builder.ToTable("BookingItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity)
               .IsRequired();

        builder.Property(x => x.UnitPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.HasOne(x => x.Booking)
               .WithMany(x => x.BookingItems)
               .HasForeignKey(x => x.BookingId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.TicketType)
               .WithMany()
               .HasForeignKey(x => x.TicketTypeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}