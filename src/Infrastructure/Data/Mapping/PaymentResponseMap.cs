namespace Infraestructure.Data.Mapping;

public class PaymentResponseMap : IEntityTypeConfiguration<PaymentResponse>
{
    public void Configure(EntityTypeBuilder<PaymentResponse> builder)
    {
        builder.ToTable("PaymentResponse");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Message)
            .HasMaxLength(255);

        builder.Property(p => p.Reference)
            .HasMaxLength(100);

        builder.Property(p => p.AuthorizationCode)
            .HasMaxLength(50);

        builder.Property(p => p.Nsu)
            .HasMaxLength(50);

        builder.Property(p => p.ReasonCode)
            .HasMaxLength(20);
    }
}
