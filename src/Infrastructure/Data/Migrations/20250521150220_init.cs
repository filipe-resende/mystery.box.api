using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SteamCardCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitPrice = table.Column<float>(type: "real", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamCardCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificationType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MercadoPagoPaymentId = table.Column<long>(type: "bigint", nullable: false),
                    ExternalReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusDetail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Installments = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardLastFour = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CardFirstSix = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PayerEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PayerDocumentType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PayerDocumentNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FullResponseJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SteamCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SteamCardCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SteamCard_SteamCardCategory_SteamCardCategoryId",
                        column: x => x.SteamCardCategoryId,
                        principalTable: "SteamCardCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SteamCard_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamCardCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseHistory_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseHistory_SteamCardCategory_SteamCardCategoryId",
                        column: x => x.SteamCardCategoryId,
                        principalTable: "SteamCardCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SteamCardCategory",
                columns: new[] { "Id", "Active", "CategoryId", "CreatedAt", "Description", "PictureUrl", "Title", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("d54c727f-e6de-4d0f-a3f8-f102061b300a"), true, "electronics", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Cartão Steam de R$50", "/img/games/1.jpg", "Basic", 29.99f },
                    { new Guid("d54c727f-e6de-4d0f-a3f8-f102061b300b"), true, "electronics", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Cartão Steam de R$50", "/img/games/2.jpg", "Premiun", 49.99f },
                    { new Guid("d54c727f-e6de-4d0f-a3f8-f102061b300c"), true, "electronics", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Cartão Steam de R$50", "/img/games/3.jpg", "Master", 89.99f }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "IdentificationNumber", "IdentificationType", "Active", "CreatedAt", "Email", "LastName", "Name", "Password", "Phone", "Role" },
                values: new object[] { new Guid("a85a95bd-9448-4945-b621-9f5b6b75e329"), "12345678900", "CPF", true, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "admin@email.com", "Castro", "Filipe", "$2a$11$acG/4uZ/MTcq2rJwE/nwbuHKS1FvUQBM.mEQrEr1qQ.A6lzjLyRzm", "31999999999", 1 });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "Amount", "ApprovedAt", "CardFirstSix", "CardHolderName", "CardLastFour", "CreatedAt", "CreatedOn", "ExternalReference", "FullResponseJson", "Installments", "MercadoPagoPaymentId", "NetAmount", "PayerDocumentNumber", "PayerDocumentType", "PayerEmail", "PaymentMethodId", "PaymentTypeId", "ReleaseDate", "Status", "StatusDetail", "UserId" },
                values: new object[] { new Guid("97f1f9f2-cd2e-41c4-b470-51f6be5b1f8b"), 5000m, new DateTime(2024, 1, 1, 12, 1, 0, 0, DateTimeKind.Utc), "123456", "Teste Da Silva", "12346", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "REF-001", "{}", 1, 123456789L, 4900m, "12345678900", "CPF", "cliente@teste.com", "pix", "pix", new DateTime(2024, 1, 3, 12, 1, 0, 0, DateTimeKind.Utc), "approved", "accredited", new Guid("a85a95bd-9448-4945-b621-9f5b6b75e329") });

            migrationBuilder.InsertData(
                table: "SteamCard",
                columns: new[] { "Id", "Active", "CreatedAt", "Description", "Key", "Name", "Status", "SteamCardCategoryId", "UserId" },
                values: new object[] { new Guid("c9f75c8d-3d83-4eaf-8487-9eaa1b3b46ed"), true, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Chave ativável no Steam", "STEAM-XYZ-123", "Cartão R$50 - Código XYZ", 0, new Guid("d54c727f-e6de-4d0f-a3f8-f102061b300a"), new Guid("a85a95bd-9448-4945-b621-9f5b6b75e329") });

            migrationBuilder.InsertData(
                table: "PurchaseHistory",
                columns: new[] { "Id", "PaymentId", "Quantity", "Status", "SteamCardCategoryId", "UnitPrice", "UserId" },
                values: new object[] { new Guid("475925bb-8bf1-4cfa-8b3b-42b16dcbe0e5"), new Guid("97f1f9f2-cd2e-41c4-b470-51f6be5b1f8b"), 1m, "Pending", new Guid("d54c727f-e6de-4d0f-a3f8-f102061b300a"), 19m, new Guid("a85a95bd-9448-4945-b621-9f5b6b75e329") });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistory_PaymentId",
                table: "PurchaseHistory",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistory_SteamCardCategoryId",
                table: "PurchaseHistory",
                column: "SteamCardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistory_UserId",
                table: "PurchaseHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SteamCard_SteamCardCategoryId",
                table: "SteamCard",
                column: "SteamCardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SteamCard_UserId",
                table: "SteamCard",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseHistory");

            migrationBuilder.DropTable(
                name: "SteamCard");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "SteamCardCategory");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
