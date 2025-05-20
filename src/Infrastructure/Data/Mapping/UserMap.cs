namespace Infraestructure.Data.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(60);

        builder.Property(p => p.LastName)
               .IsRequired()
               .HasMaxLength(60);

        builder.Property(p => p.Password)
               .IsRequired();

        builder.Property(p => p.Email)
               .IsRequired();

        builder.Property(p => p.Phone)
               .HasMaxLength(20);

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.Active)
               .HasDefaultValue(true);

        builder.Property(p => p.Role)
               .IsRequired();

        builder.OwnsOne(p => p.Identification, id =>
        {
            id.Property(i => i.Type)
              .HasColumnName("IdentificationType")
              .IsRequired()
              .HasMaxLength(20);

            id.Property(i => i.Number)
              .HasColumnName("IdentificationNumber")
              .IsRequired()
              .HasMaxLength(20);
        });

        builder.HasMany(p => p.SteamCards)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId);

        builder.HasMany(p => p.Payments)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId);
    }
}
