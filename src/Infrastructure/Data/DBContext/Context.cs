namespace Infraestructure.Data.DBContext;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<SteamCard> SteamCards { get; set; }
    public DbSet<SteamCardCategory> SteamCardCategories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new SteamCardMap());
        modelBuilder.ApplyConfiguration(new SteamCardCategoryMap());
        modelBuilder.ApplyConfiguration(new PaymentsMap());
        modelBuilder.ApplyConfiguration(new PurchaseHistoryMap());

        var userId = Guid.Parse("a85a95bd-9448-4945-b621-9f5b6b75e329");
        var categoryId = Guid.Parse("d54c727f-e6de-4d0f-a3f8-f102061b300a");
        var purchaseHistoryId = Guid.Parse("475925bb-8bf1-4cfa-8b3b-42b16dcbe0e5");
        var steamCardId = Guid.Parse("c9f75c8d-3d83-4eaf-8487-9eaa1b3b46ed");
        var paymentId = Guid.Parse("97f1f9f2-cd2e-41c4-b470-51f6be5b1f8b");
        var createdAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc);
        var paidAt = new DateTime(2024, 01, 01, 12, 30, 0, DateTimeKind.Utc);
        var approvedAt = createdAt.AddMinutes(1);

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = userId,
            Name = "Filipe",
            LastName = "Castro",
            Email = "admin@email.com",
            Password = "$2a$11$acG/4uZ/MTcq2rJwE/nwbuHKS1FvUQBM.mEQrEr1qQ.A6lzjLyRzm",
            Phone = "31999999999",
            CreatedAt = createdAt,
            Role = Role.Registered,
            Active = true
        });

        modelBuilder.Entity<User>().OwnsOne(u => u.Identification).HasData(new
        {
            UserId = userId, 
            Type = "CPF",
            Number = "12345678900"
        });

        modelBuilder.Entity<SteamCardCategory>().HasData(
            new SteamCardCategory
            {
                Id = categoryId,
                Title = "Basic",
                UnitPrice = 29.99f,
                PictureUrl = "/img/games/1.jpg",
                Description = "Cartão Steam de R$50",
                Active = true,
                CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0),
                CategoryId = "electronics"
            },
            new SteamCardCategory
            {
                Id = Guid.Parse("D54C727F-E6DE-4D0F-A3F8-F102061B300B"),
                Title = "Premiun",
                UnitPrice = 49.99f,
                PictureUrl = "/img/games/2.jpg",
                Description = "Cartão Steam de R$50",
                Active = true,
                CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0),
                CategoryId = "electronics"

            },
            new SteamCardCategory
            {
                Id = Guid.Parse("D54C727F-E6DE-4D0F-A3F8-F102061B300C"),
                Title = "Master",
                UnitPrice = 89.99f,
                PictureUrl = "/img/games/3.jpg",
                Description = "Cartão Steam de R$50",
                Active = true,
                CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0),
                CategoryId = "electronics"

            }
        );


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

        modelBuilder.Entity<PurchaseHistory>().HasData(new PurchaseHistory
        {
            Id = purchaseHistoryId,
            UserId = userId,
            PaymentId = paymentId,
            SteamCardCategoryId = categoryId, 
            Quantity = 1,
            UnitPrice = 19,
            Status = SteamCardStatus.Pending
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Id = paymentId,
            MercadoPagoPaymentId = 123456789,
            ExternalReference = "REF-001",
            Status = "approved",
            StatusDetail = "accredited",
            CreatedAt = createdAt,
            ApprovedAt = approvedAt,
            ReleaseDate = approvedAt.AddDays(2),
            Amount = 5000m,
            NetAmount = 4900m,
            Installments = 1,
            PaymentMethodId = "pix",
            PaymentTypeId = "pix",
            CardLastFour = "12346",
            CardFirstSix = "123456",
            CardHolderName = "Teste Da Silva",
            PayerEmail = "cliente@teste.com",
            PayerDocumentType = "CPF",
            PayerDocumentNumber = "12345678900",
            FullResponseJson = "{}", 
            CreatedOn = createdAt,
            UserId = userId
        });
    }
}
