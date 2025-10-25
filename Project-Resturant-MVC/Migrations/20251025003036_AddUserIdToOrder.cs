using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Resturant_MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MenuItems");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(520));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(523));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(524));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(526));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(766));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(769));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(772));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(775));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(777));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(780));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(782));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(785));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(787));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(789));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(792));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(794));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(797));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(894));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(898));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(901));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(903));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(905));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(853), null });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 30, 31, 958, DateTimeKind.Utc).AddTicks(856), null });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MenuItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5167));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5169));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5170));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5325), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5329), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5332), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5335), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5337), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5340), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5342), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5345), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5347), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5350), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5352), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5355), null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5357), null });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5520));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5523));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5526));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5528));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5530));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5407));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 14, 41, 33, 312, DateTimeKind.Utc).AddTicks(5410));
        }
    }
}
