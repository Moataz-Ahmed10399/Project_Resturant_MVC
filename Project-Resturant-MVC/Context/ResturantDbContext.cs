using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Models;

namespace Project_Resturant_MVC.Context
{
    public class ResturantDbContext : IdentityDbContext<ApplicationUser>
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //HardCode
        //    optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Database=RestaurantDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        //}
        public ResturantDbContext(DbContextOptions<ResturantDbContext> options)
         : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Global Query Filter للـ Soft Delete
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<MenuItem>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDeleted);
            modelBuilder.Entity<OrderItem>().HasQueryFilter(oi => !oi.IsDeleted);

            // Category -> MenuItem (Cascade Delete)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.MenuItems)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order -> OrderItem (Cascade Delete)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // MenuItem -> OrderItem (Restrict Delete)
            modelBuilder.Entity<MenuItem>()
                .HasMany(m => m.OrderItems)
                .WithOne(oi => oi.MenuItem)
                .HasForeignKey(oi => oi.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);


            SeedData(modelBuilder);

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
         
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Appetizers",
                    Description = "Start your meal with our delicious appetizers",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Category
                {
                    Id = 2,
                    Name = "Main Courses",
                    Description = "Hearty and satisfying main dishes",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Category
                {
                    Id = 3,
                    Name = "Desserts",
                    Description = "Sweet treats to end your meal",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Category
                {
                    Id = 4,
                    Name = "Beverages",
                    Description = "Refreshing drinks",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                }
            );
         
            modelBuilder.Entity<MenuItem>().HasData(
                // Appetizers
                new MenuItem
                {
                    Id = 1,
                    Name = "Chicken Wings",
                    Description = "Crispy wings with buffalo sauce",
                    Price = 45.00m,
                    PreparationTimeMinutes = 15,
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 10,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 2,
                    Name = "Mozzarella Sticks",
                    Description = "Fried cheese sticks with marinara",
                    Price = 35.00m,
                    PreparationTimeMinutes = 10,
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 8,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 3,
                    Name = "Caesar Salad",
                    Description = "Fresh romaine with parmesan and croutons",
                    Price = 40.00m,
                    PreparationTimeMinutes = 8,
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 7,
                    ImageUrl = null,
                    IsDeleted = false
                },

                // Main Courses
                new MenuItem
                {
                    Id = 4,
                    Name = "Margherita Pizza",
                    Description = "Classic pizza with tomato, mozzarella, and basil",
                    Price = 85.00m,
                    PreparationTimeMinutes = 20,
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 10,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 5,
                    Name = "Beef Burger",
                    Description = "Juicy beef patty with cheese and fries",
                    Price = 95.00m,
                    PreparationTimeMinutes = 18,
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 10,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 6,
                    Name = "Pasta Carbonara",
                    Description = "Creamy pasta with bacon and parmesan",
                    Price = 80.00m,
                    PreparationTimeMinutes = 15,
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 6,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 7,
                    Name = "Grilled Chicken",
                    Description = "Marinated chicken breast with vegetables",
                    Price = 110.00m,
                    PreparationTimeMinutes = 25,
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 12,
                    ImageUrl = null,
                    IsDeleted = false
                },

                // Desserts
                new MenuItem
                {
                    Id = 8,
                    Name = "Chocolate Cake",
                    Description = "Rich chocolate layer cake",
                    Price = 50.00m,
                    PreparationTimeMinutes = 5,
                    IsAvailable = true,
                    CategoryId = 3,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 20,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 9,
                    Name = "Cheesecake",
                    Description = "Creamy New York style cheesecake",
                    Price = 55.00m,
                    PreparationTimeMinutes = 5,
                    IsAvailable = true,
                    CategoryId = 3,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 15,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 10,
                    Name = "Ice Cream Sundae",
                    Description = "Three scoops with toppings",
                    Price = 40.00m,
                    PreparationTimeMinutes = 3,
                    IsAvailable = true,
                    CategoryId = 3,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 10,
                    ImageUrl = null,
                    IsDeleted = false
                },

                // Beverages
                new MenuItem
                {
                    Id = 11,
                    Name = "Fresh Orange Juice",
                    Description = "Freshly squeezed orange juice",
                    Price = 25.00m,
                    PreparationTimeMinutes = 3,
                    IsAvailable = true,
                    CategoryId = 4,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 10,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 12,
                    Name = "Cappuccino",
                    Description = "Italian style coffee with foam",
                    Price = 30.00m,
                    PreparationTimeMinutes = 5,
                    IsAvailable = true,
                    CategoryId = 4,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 10,
                    ImageUrl = null,
                    IsDeleted = false
                },
                new MenuItem
                {
                    Id = 13,
                    Name = "Soft Drink",
                    Description = "Coca Cola, Sprite, or Fanta",
                    Price = 15.00m,
                    PreparationTimeMinutes = 1,
                    IsAvailable = true,
                    CategoryId = 4,
                    CreatedAt = DateTime.UtcNow,
                    Quantity = 10,
                    ImageUrl = null,
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<Order>().HasData(
                    new Order
                    {
                        Id = 1,
                        CustomerName = "Ahmed Ali",
                        CustomerPhone = "01012345678",
                        DeliveryAddress = "Cairo, Nasr City",
                        Subtotal = 180.00m,
                        Tax = 0.00m,
                        Discount = 0.00m,
                        Total = 180.00m,
                        Status = OrderStatus.Delivered, // ✅ بدل Completed
                        Type = OrderType.DineIn,
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    },
                    new Order
                    {
                        Id = 2,
                        CustomerName = "Sara Mohamed",
                        CustomerPhone = "01098765432",
                        DeliveryAddress = "Giza, Dokki",
                        Subtotal = 95.00m,
                        Tax = 0.00m,
                        Discount = 0.00m,
                        Total = 95.00m,
                        Status = OrderStatus.Pending,
                        Type = OrderType.Takeaway,
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    }
                );

            modelBuilder.Entity<OrderItem>().HasData(
                // Order 1 items
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    MenuItemId = 5, // Beef Burger
                    Quantity = 1,
                    UnitPrice = 95.00m,
                    Subtotal = 95.00m,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    MenuItemId = 8, // Chocolate Cake
                    Quantity = 1,
                    UnitPrice = 50.00m,
                    Subtotal = 50.00m,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
                new OrderItem
                {
                    Id = 3,
                    OrderId = 1,
                    MenuItemId = 11, // Fresh Orange Juice
                    Quantity = 1,
                    UnitPrice = 25.00m,
                    Subtotal = 25.00m,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },

                // Order 2 items
                new OrderItem
                {
                    Id = 4,
                    OrderId = 2,
                    MenuItemId = 4, // Margherita Pizza
                    Quantity = 1,
                    UnitPrice = 85.00m,
                    Subtotal = 85.00m,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                },
                new OrderItem
                {
                    Id = 5,
                    OrderId = 2,
                    MenuItemId = 13, // Soft Drink
                    Quantity = 1,
                    UnitPrice = 10.00m,
                    Subtotal = 10.00m,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                }
            );


        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
