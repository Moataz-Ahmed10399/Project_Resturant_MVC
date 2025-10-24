using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Resturant_MVC.Migrations
{
    /// <inheritdoc />
    public partial class QuantityMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2437));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2439));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2440));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2441));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2543), 10 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2545), 8 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2547), 7 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2549), 10 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2551), 10 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2553), 6 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2554), 12 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2556), 20 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2558), 15 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2559), 10 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2561), 10 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2562), 10 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2564), 10 });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2618));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2621));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2622));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2624));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2625));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2594));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 22, 2, 8, 277, DateTimeKind.Utc).AddTicks(2597));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MenuItems");

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

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2031));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2033));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2034));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2035));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 15, 20, 50, 52, 219, DateTimeKind.Utc).AddTicks(2008));
        }
    }
}
