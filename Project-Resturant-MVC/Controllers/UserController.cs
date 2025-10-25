using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Models;

namespace Project_Resturant_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            // جلب Role كل User
            var usersWithRoles = new List<(ApplicationUser User, string Role)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add((user, roles.FirstOrDefault() ?? "No Role"));
            }

            return View(usersWithRoles);
        }

 
        public async Task<IActionResult> ChangeRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            ViewBag.UserName = user.UserName;
            ViewBag.CurrentRole = currentRoles.FirstOrDefault() ?? "No Role";
            ViewBag.UserId = userId;
            ViewBag.AllRoles = new SelectList(allRoles);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, newRole);

            TempData["SuccessMessage"] = $"User '{user.UserName}' role changed to '{newRole}'";
            return RedirectToAction("GetAllUsers");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (user.UserName == "admin")
            {
                TempData["ErrorMessage"] = "Cannot delete the main admin account.";
                return RedirectToAction("GetAllUsers");
            }

            await _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = $"User '{user.UserName}' deleted successfully.";
            return RedirectToAction("GetAllUsers");
        }
    }
}
