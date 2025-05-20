namespace Infraestructure.Data.Mapping;
public class PaymentsMap : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.MercadoPagoPaymentId)
               .IsRequired();

        builder.Property(p => p.ExternalReference)
               .HasMaxLength(100);

        builder.Property(p => p.Status)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.StatusDetail)
               .HasMaxLength(100);

        builder.Property(p => p.Amount)
                .HasPrecision(18, 4)
                .IsRequired();

        builder.Property(p => p.NetAmount)
               .HasPrecision(18, 4)
               .IsRequired();

        builder.Property(p => p.PaymentMethodId)
               .HasMaxLength(50);

        builder.Property(p => p.PaymentTypeId)
               .HasMaxLength(50);

        builder.Property(p => p.CardLastFour)
               .HasMaxLength(10);

        builder.Property(p => p.CardFirstSix)
               .HasMaxLength(10);

        builder.Property(p => p.CardHolderName)
               .HasMaxLength(100);

        builder.Property(p => p.PayerEmail)
               .HasMaxLength(150);

        builder.Property(p => p.PayerDocumentType)
               .HasMaxLength(10);

        builder.Property(p => p.PayerDocumentNumber)
               .HasMaxLength(30);

        builder.Property(p => p.FullResponseJson)
               .HasColumnType("nvarchar(max)");

        builder.Property(p => p.CreatedOn)
               .HasDefaultValueSql("GETDATE()");

        builder.HasOne(p => p.User)
               .WithMany(u => u.Payments)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}

