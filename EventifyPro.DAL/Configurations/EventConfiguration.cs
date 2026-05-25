namespace EventifyPro.DAL.Configurations;

/// <summary>
/// Entity Framework configuration for the Event entity.
/// </summary>
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    /// <summary>
    /// Configures the Event entity in the model.
    /// </summary>
    /// <param name="builder">The builder for configuring the entity type.</param>
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Events_EndDate",
                "EndDate > StartDate");
        });

        builder.Property(e => e.Location)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(e => e.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ImageUrl)
            .HasMaxLength(500);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<byte>()
            .HasDefaultValueSql("0");

        builder.Property(e => e.MaxCapacity);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Events_MaxCapacity",
                "MaxCapacity IS NULL OR MaxCapacity > 0");
        });

        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.UpdatedAt)
            .HasDefaultValue(null);

        builder.HasOne(e => e.Organizer)
            .WithMany(u => u.OrganizedEvents)
            .HasForeignKey(e => e.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Category)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(e => e.OrganizerId)
            .HasDatabaseName("IX_Events_OrganizerId");

        builder.HasIndex(e => e.CategoryId)
            .HasDatabaseName("IX_Events_CategoryId");

        builder.HasIndex(e => new { e.Status, e.StartDate })
            .HasDatabaseName("IX_Events_Status_StartDate");

        builder.HasIndex(e => e.City)
            .HasDatabaseName("IX_Events_City");
    }
}
