namespace Infraestructure.Data.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(60);

        builder.Property(p => p.Password)
               .IsRequired();

        builder.Property(p => p.Email)
               .IsRequired();

        builder.Property(p => p.CPF)
               .IsRequired();

        builder.Property(p => p.Phone);

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.Active)
               .HasDefaultValue(true);

        builder.HasMany(p => p.SteamCards)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId);

        builder.HasMany(p => p.Payments)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId);
    }
}
