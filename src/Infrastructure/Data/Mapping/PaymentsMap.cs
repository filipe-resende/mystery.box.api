namespace Infraestructure.Data.Mapping;

public class PaymentsMap : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ReferenceId)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Status)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.Description)
               .HasMaxLength(500);

        builder.Property(p => p.AmountValue)
               .IsRequired();

        builder.Property(p => p.AmountCurrency)
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.PaidAt)
               .IsRequired(false);

        builder.HasOne(p => p.User)
               .WithMany(u => u.Payments)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}