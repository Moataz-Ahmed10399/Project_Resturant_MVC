using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Resturant_MVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7194), "Start your meal with our delicious appetizers", false, "Appetizers", null },
                    { 2, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7197), "Hearty and satisfying main dishes", false, "Main Courses", null },
                    { 3, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7199), "Sweet treats to end your meal", false, "Desserts", null },
                    { 4, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7200), "Refreshing drinks", false, "Beverages", null }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "IsAvailable", "IsDeleted", "Name", "PreparationTimeMinutes", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7355), "Crispy wings with buffalo sauce", true, false, "Chicken Wings", 15, 45.00m, null },
                    { 2, 1, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7357), "Fried cheese sticks with marinara", true, false, "Mozzarella Sticks", 10, 35.00m, null },
                    { 3, 1, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7359), "Fresh romaine with parmesan and croutons", true, false, "Caesar Salad", 8, 40.00m, null },
                    { 4, 2, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7360), "Classic pizza with tomato, mozzarella, and basil", true, false, "Margherita Pizza", 20, 85.00m, null },
                    { 5, 2, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7362), "Juicy beef patty with cheese and fries", true, false, "Beef Burger", 18, 95.00m, null },
                    { 6, 2, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7364), "Creamy pasta with bacon and parmesan", true, false, "Pasta Carbonara", 15, 80.00m, null },
                    { 7, 2, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7365), "Marinated chicken breast with vegetables", true, false, "Grilled Chicken", 25, 110.00m, null },
                    { 8, 3, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7367), "Rich chocolate layer cake", true, false, "Chocolate Cake", 5, 50.00m, null },
                    { 9, 3, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7368), "Creamy New York style cheesecake", true, false, "Cheesecake", 5, 55.00m, null },
                    { 10, 3, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7370), "Three scoops with toppings", true, false, "Ice Cream Sundae", 3, 40.00m, null },
                    { 11, 4, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7371), "Freshly squeezed orange juice", true, false, "Fresh Orange Juice", 3, 25.00m, null },
                    { 12, 4, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7373), "Italian style coffee with foam", true, false, "Cappuccino", 5, 30.00m, null },
                    { 13, 4, new DateTime(2025, 10, 15, 20, 39, 38, 660, DateTimeKind.Utc).AddTicks(7374), "Coca Cola, Sprite, or Fanta", true, false, "Soft Drink", 1, 15.00m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
