namespace Infraestructure.Data.Mapping;

public class SteamCardMap : IEntityTypeConfiguration<SteamCard>
{
    public void Configure(EntityTypeBuilder<SteamCard> builder)
    {
        builder.ToTable("SteamCard");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Key)
               .HasMaxLength(255);

        builder.Property(p => p.Description)
               .HasMaxLength(1000);

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.Active)
               .HasDefaultValue(true);

        builder.HasOne(p => p.User)
               .WithMany(u => u.SteamCards)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.SteamCardCategory)
               .WithMany()
               .HasForeignKey(p => p.SteamCardCategoryId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}

