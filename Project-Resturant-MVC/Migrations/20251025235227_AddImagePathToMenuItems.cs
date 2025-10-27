using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Resturant_MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToMenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3003));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3006));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3207), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3209), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3211), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3213), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3214), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3216), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3218), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3220), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3221), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3223), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3225), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3226), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3228), null });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3301));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3302));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3304));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3305));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3267));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 23, 52, 23, 708, DateTimeKind.Utc).AddTicks(3270));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "MenuItems");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9804));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9807));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9809));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9810));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9960));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9963));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9965));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9966));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9968));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9970));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9971));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9973));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9974));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9976));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9977));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9979));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 877, DateTimeKind.Utc).AddTicks(9981));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 878, DateTimeKind.Utc).AddTicks(46));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 878, DateTimeKind.Utc).AddTicks(48));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 878, DateTimeKind.Utc).AddTicks(50));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 878, DateTimeKind.Utc).AddTicks(51));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 878, DateTimeKind.Utc).AddTicks(53));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 878, DateTimeKind.Utc).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 20, 50, 26, 878, DateTimeKind.Utc).AddTicks(22));
        }
    }
}
