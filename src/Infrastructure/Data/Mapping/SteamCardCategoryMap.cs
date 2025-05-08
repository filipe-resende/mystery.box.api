namespace Infraestructure.Data.Mapping;

public class SteamCardCategoryMap : IEntityTypeConfiguration<SteamCardCategory>
{
    public void Configure(EntityTypeBuilder<SteamCardCategory> builder)
    {
        builder.ToTable("SteamCardCategory");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Price)
               .IsRequired();

        builder.Property(p => p.Thumb)
               .HasMaxLength(255);

        builder.Property(p => p.Description)
               .HasMaxLength(1000);

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.Active)
               .HasDefaultValue(true);
    }
}
