using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AuthorizationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nsu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReasonCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SteamCardCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Thumb = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
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
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    ReferenceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AmountValue = table.Column<int>(type: "int", nullable: false),
                    AmountCurrency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PaymentResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_PaymentResponse_PaymentResponseId",
                        column: x => x.PaymentResponseId,
                        principalTable: "PaymentResponse",
                        principalColumn: "Id");
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
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

            migrationBuilder.InsertData(
                table: "PaymentResponse",
                columns: new[] { "Id", "AuthorizationCode", "Code", "Message", "Nsu", "ReasonCode", "Reference" },
                values: new object[] { new Guid("475925bb-8bf1-4cfa-8b3b-42b16dcbe0e5"), "AUTH123", "00", "Aprovado", "NSU456", "00", "REF-001" });

            migrationBuilder.InsertData(
                table: "SteamCardCategory",
                columns: new[] { "Id", "Active", "CreatedAt", "Description", "Name", "Price", "Thumb" },
                values: new object[] { new Guid("d54c727f-e6de-4d0f-a3f8-f102061b300a"), true, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Cartão Steam de R$50", "Steam R$50", 50f, "/img/steam50.png" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Active", "CPF", "CreatedAt", "Email", "Name", "Password", "Phone" },
                values: new object[] { new Guid("a85a95bd-9448-4945-b621-9f5b6b75e329"), true, "12345678900", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "filipe@email.com", "Filipe Castro", "123456", "31999999999" });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "AmountCurrency", "AmountValue", "CreatedAt", "Description", "PaidAt", "PaymentResponseId", "ReferenceId", "Status", "UserId" },
                values: new object[] { new Guid("97f1f9f2-cd2e-41c4-b470-51f6be5b1f8b"), "BRL", 5000, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Pagamento via Pix", new DateTime(2024, 1, 1, 12, 30, 0, 0, DateTimeKind.Utc), new Guid("475925bb-8bf1-4cfa-8b3b-42b16dcbe0e5"), "REF-001", "Paid", new Guid("a85a95bd-9448-4945-b621-9f5b6b75e329") });

            migrationBuilder.InsertData(
                table: "SteamCard",
                columns: new[] { "Id", "Active", "CreatedAt", "Description", "Key", "Name", "SteamCardCategoryId", "UserId" },
                values: new object[] { new Guid("c9f75c8d-3d83-4eaf-8487-9eaa1b3b46ed"), true, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Chave ativável no Steam", "STEAM-XYZ-123", "Cartão R$50 - Código XYZ", new Guid("d54c727f-e6de-4d0f-a3f8-f102061b300a"), new Guid("a85a95bd-9448-4945-b621-9f5b6b75e329") });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentResponseId",
                table: "Payment",
                column: "PaymentResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
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
                name: "Payment");

            migrationBuilder.DropTable(
                name: "SteamCard");

            migrationBuilder.DropTable(
                name: "PaymentResponse");

            migrationBuilder.DropTable(
                name: "SteamCardCategory");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
