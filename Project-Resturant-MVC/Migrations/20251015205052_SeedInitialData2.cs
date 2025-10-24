using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Resturant_MVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1801));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1802));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1803));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1805));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1952));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1954));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1955));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1957));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1959));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1962));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1963));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1965));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1966));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1968));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1969));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(1971));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerName", "CustomerPhone", "DeliveredAt", "DeliveryAddress", "Discount", "EstimatedDeliveryMinutes", "IsDeleted", "ReadyAt", "Status", "Subtotal", "Tax", "Total", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2005), "Ahmed Ali", "01012345678", null, "Cairo, Nasr City", 0.00m, null, false, null, 3, 180.00m, 0.00m, 180.00m, 0, null },
                    { 2, new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2008), "Sara Mohamed", "01098765432", null, "Giza, Dokki", 0.00m, null, false, null, 0, 95.00m, 0.00m, 95.00m, 1, null }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "MenuItemId", "OrderId", "Quantity", "Subtotal", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2028), false, 5, 1, 1, 95.00m, 95.00m, null },
                    { 2, new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2031), false, 8, 1, 1, 50.00m, 50.00m, null },
                    { 3, new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2033), false, 11, 1, 1, 25.00m, 25.00m, null },
                    { 4, new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2034), false, 4, 2, 1, 85.00m, 85.00m, null },
                    { 5, new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2035), false, 13, 2, 1, 10.00m, 10.00m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7194));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7197));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7199));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7200));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7359));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7362));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7364));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7365));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7367));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7368));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7371));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7373));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7374));
        }
    }
}
