namespace Infraestructure.Data.DBContext;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<SteamCard> SteamCards { get; set; }
    public DbSet<SteamCardCategory> SteamCardCategories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentResponse> PaymentResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new SteamCardMap());
        modelBuilder.ApplyConfiguration(new SteamCardCategoryMap());
        modelBuilder.ApplyConfiguration(new PaymentsMap());
        modelBuilder.ApplyConfiguration(new PaymentResponseMap());

        var userId = Guid.Parse("a85a95bd-9448-4945-b621-9f5b6b75e329");
        var categoryId = Guid.Parse("d54c727f-e6de-4d0f-a3f8-f102061b300a");
        var paymentResponseId = Guid.Parse("475925bb-8bf1-4cfa-8b3b-42b16dcbe0e5");
        var steamCardId = Guid.Parse("c9f75c8d-3d83-4eaf-8487-9eaa1b3b46ed");
        var paymentId = Guid.Parse("97f1f9f2-cd2e-41c4-b470-51f6be5b1f8b");

        var createdAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc);
        var paidAt = new DateTime(2024, 01, 01, 12, 30, 0, DateTimeKind.Utc);

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = userId,
            Name = "Filipe Castro",
            Email = "filipe@email.com",
            Password = "$2a$11$IcNvB5j1k5dmwIviLHF48uu5j.6u4S1Thy/xW6zkTyug9OaX56YgC",
            CPF = "12345678900",
            Phone = "31999999999",
            CreatedAt = createdAt,
            Role = Role.Registered,
            Active = true
        });

        modelBuilder.Entity<SteamCardCategory>().HasData(new SteamCardCategory
        {
            Id = categoryId,
            Name = "Steam R$50",
            Price = 50.0f,
            Thumb = "/games/2.jpg",
            Description = "Cartão Steam de R$50",
            Active = true,
            CreatedAt = createdAt
        });

        modelBuilder.Entity<SteamCard>().HasData(new SteamCard
        {
            Id = steamCardId,
            Name = "Cartão R$50 - Código XYZ",
            Key = "STEAM-XYZ-123",
            Description = "Chave ativável no Steam",
            Active = true,
            CreatedAt = createdAt,
            UserId = userId,
            SteamCardCategoryId = categoryId
        });

        modelBuilder.Entity<PaymentResponse>().HasData(new PaymentResponse
        {
            Id = paymentResponseId,
            Code = "00",
            Message = "Aprovado",
            Reference = "REF-001",
            AuthorizationCode = "AUTH123",
            Nsu = "NSU456",
            ReasonCode = "00"
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Id = paymentId,
            ReferenceId = "REF-001",
            Status = "Paid",
            CreatedAt = createdAt,
            PaidAt = paidAt,
            Description = "Pagamento via Pix",
            AmountValue = 5000,
            AmountCurrency = "BRL",
            UserId = userId,
            PaymentResponseId = paymentResponseId
        });
    }
}
