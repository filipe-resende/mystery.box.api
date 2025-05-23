namespace Infrastructure.Data.Mapping;

public class SteamCardCategoryMap : IEntityTypeConfiguration<SteamCardCategory>
{
    public void Configure(EntityTypeBuilder<SteamCardCategory> builder)
    {
        builder.ToTable("SteamCardCategory");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.UnitPrice)
               .IsRequired();

        builder.Property(p => p.PictureUrl)
               .HasMaxLength(255);

        builder.Property(p => p.Description)
               .HasMaxLength(1000);

        builder.Property(p => p.CategoryId)
             .HasMaxLength(1000);

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.Active)
               .HasDefaultValue(true);
    }
}
