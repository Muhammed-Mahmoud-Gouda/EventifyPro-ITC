namespace EventifyPro.DAL.Configurations;

/// <summary>
/// Entity Framework configuration for the OutboxMessage entity.
/// </summary>
public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    /// <summary>
    /// Configures the OutboxMessage entity in the model.
    /// </summary>
    /// <param name="builder">The builder for configuring the entity type.</param>
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        // Primary Key
        builder.HasKey(o => o.Id);

        // Properties
        builder.Property(o => o.Type)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");

        builder.Property(o => o.Payload)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(o => o.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(o => o.ScheduledFor)
            .IsRequired(false);

        builder.Property(o => o.ProcessedAt)
            .IsRequired(false);

        builder.Property(o => o.RetryCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(o => o.LastError)
            .IsRequired(false)
            .HasMaxLength(1000)
            .HasColumnType("nvarchar(1000)");

        // Indexes for efficient polling and querying
        builder.HasIndex(o => o.ProcessedAt)
            .HasDatabaseName("IX_OutboxMessages_ProcessedAt")
            .IsUnique(false);

        builder.HasIndex(o => new { o.ScheduledFor, o.ProcessedAt })
            .HasDatabaseName("IX_OutboxMessages_ScheduledFor_ProcessedAt")
            .IsUnique(false);

        builder.HasIndex(o => o.CreatedAt)
            .HasDatabaseName("IX_OutboxMessages_CreatedAt")
            .IsUnique(false);

        // Table configuration
        builder.ToTable("OutboxMessages", "dbo");
    }
}
