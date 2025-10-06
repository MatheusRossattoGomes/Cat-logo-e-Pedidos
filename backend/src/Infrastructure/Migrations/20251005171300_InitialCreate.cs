using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Document = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    StockQty = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    LineTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "Document", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "11144455566", "cliente1@email.com", "Cliente 1" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "22244455566", "cliente2@email.com", "Cliente 2" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "33344455566", "cliente3@email.com", "Cliente 3" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "44444455566", "cliente4@email.com", "Cliente 4" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "55544455566", "cliente5@email.com", "Cliente 5" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "66644455566", "cliente6@email.com", "Cliente 6" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "77744455566", "cliente7@email.com", "Cliente 7" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "88844455566", "cliente8@email.com", "Cliente 8" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "99944455566", "cliente9@email.com", "Cliente 9" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), "10101044455566", "cliente10@email.com", "Cliente 10" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "Price", "Sku", "StockQty" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-000000000001"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 1", 10m, "SKU-PROD-001", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000002"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 2", 20m, "SKU-PROD-002", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000003"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 3", 30m, "SKU-PROD-003", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000004"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 4", 40m, "SKU-PROD-004", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000005"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 5", 50m, "SKU-PROD-005", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000006"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 6", 60m, "SKU-PROD-006", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000007"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 7", 70m, "SKU-PROD-007", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000008"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 8", 80m, "SKU-PROD-008", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000009"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 9", 90m, "SKU-PROD-009", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000010"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 10", 100m, "SKU-PROD-010", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000011"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 11", 110m, "SKU-PROD-011", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000012"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 12", 120m, "SKU-PROD-012", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000013"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 13", 130m, "SKU-PROD-013", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000014"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 14", 140m, "SKU-PROD-014", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000015"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 15", 150m, "SKU-PROD-015", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000016"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 16", 160m, "SKU-PROD-016", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000017"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 17", 170m, "SKU-PROD-017", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000018"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 18", 180m, "SKU-PROD-018", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000019"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 19", 190m, "SKU-PROD-019", 100 },
                    { new Guid("11111111-1111-1111-1111-000000000020"), new DateTime(2025, 10, 5, 14, 0, 0, 0, DateTimeKind.Utc), true, "Produto 20", 200m, "SKU-PROD-020", 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
