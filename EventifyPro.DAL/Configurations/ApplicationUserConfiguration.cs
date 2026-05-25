namespace EventifyPro.DAL.Configurations;

/// <summary>
/// Entity Framework configuration for the ApplicationUser entity.
/// </summary>
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <summary>
    /// Configures the ApplicationUser entity in the model.
    /// </summary>
    /// <param name="builder">The builder for configuring the entity type.</param>
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.ProfileImageUrl)
            .HasMaxLength(500);

        builder.Property(u => u.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(u => u.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(u => u.UpdatedAt)
            .HasDefaultValue(null);

        builder.Property(u => u.ScannerCreatedByOrganizerId)
            .HasMaxLength(450);

        builder.HasOne(u => u.ScannerCreatedByOrganizer)
            .WithMany(u => u.CreatedScannerAccounts)
            .HasForeignKey(u => u.ScannerCreatedByOrganizerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
