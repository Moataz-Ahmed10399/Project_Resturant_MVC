using Microsoft.AspNetCore.Identity;
using Project_Resturant_MVC.Models;

namespace Project_Resturant_MVC.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Client", "Kitchen" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@restaurant.com",
                    Address = "Restaurant HQ",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var kitchenUser = await userManager.FindByNameAsync("kitchen");
            if (kitchenUser == null)
            {
                kitchenUser = new ApplicationUser
                {
                    UserName = "kitchen",
                    Email = "kitchen@restaurant.com",
                    Address = "Kitchen Department",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(kitchenUser, "Kitchen@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(kitchenUser, "Kitchen");
                }
            }
        }
    }
}
