namespace Infrastructure.Data.Mapping;

public class PurchaseHistoryMap : IEntityTypeConfiguration<PurchaseHistory>
{
    public void Configure(EntityTypeBuilder<PurchaseHistory> builder)
    {
        builder.ToTable("PurchaseHistory");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Quantity)
               .HasPrecision(18, 4)
               .IsRequired();

        builder.Property(p => p.UnitPrice)
               .HasPrecision(18, 4)
               .IsRequired();

        builder.Property(p => p.Status)
               .HasConversion<string>();

        builder.HasOne(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Payment)
                .WithMany(p => p.PurchaseHistories)
                .HasForeignKey(p => p.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.SteamCardCategory)
               .WithMany()
               .HasForeignKey(p => p.SteamCardCategoryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
