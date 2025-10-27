using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;

namespace Project_Resturant_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]

    public class UserController : Controller
    {
        private readonly ResturantDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ResturantDbContext _db;
        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,ResturantDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet("")]

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

        [HttpGet("changerole/{userId}")]
        public async Task<IActionResult> ChangeRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            ViewBag.UserName = user.UserName;
            ViewBag.CurrentRole = currentRoles.FirstOrDefault() ?? "No Role";
            ViewBag.UserId = userId;
            ViewBag.AllRoles = new SelectList(allRoles);
            return View();
        }


        //[HttpPost("changerole")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangeRole(string userId, string newRole)
        //{
        //    if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(newRole))
        //    {
        //        TempData["ErrorMessage"] = "Please choose a valid role.";
        //        return RedirectToAction("GetAllUsers");
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null) return NotFound();

        //    if (!await _roleManager.RoleExistsAsync(newRole))
        //    {
        //        TempData["ErrorMessage"] = $"Role '{newRole}' does not exist.";
        //        return RedirectToAction("GetAllUsers");
        //    }

        //    var currentRoles = await _userManager.GetRolesAsync(user);
        //    if (currentRoles.Any())
        //        await _userManager.RemoveFromRolesAsync(user, currentRoles);

        //    await _userManager.AddToRoleAsync(user, newRole);

        //    TempData["SuccessMessage"] = $"User '{user.UserName}' role changed to '{newRole}'.";
        //    return RedirectToAction("GetAllUsers");
        //}
        [HttpPost("updateRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(string userId, string newRole)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(newRole))
            {
                TempData["ErrorMessage"] = "Please choose a valid role.";
                return RedirectToAction("GetAllUsers");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                TempData["ErrorMessage"] = $"Role '{newRole}' does not exist.";
                return RedirectToAction("GetAllUsers");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, newRole);

            TempData["SuccessMessage"] = $"User '{user.UserName}' role changed to '{newRole}'.";
            return RedirectToAction("GetAllUsers");
        }


        [HttpPost("deleteuser/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            if (user.Id == currentUserId)
            {
                TempData["ErrorMessage"] = "You cannot delete your own account while logged in.";
                return RedirectToAction("GetAllUsers");
            }

            if (user.UserName?.Equals("admin", StringComparison.OrdinalIgnoreCase) == true)
            {
                TempData["ErrorMessage"] = "Cannot delete the main admin account.";
                return RedirectToAction("GetAllUsers");
            }

            var hasOrders = _context.Orders.Any(o => o.UserId == userId && !o.IsDeleted);
            if (hasOrders)
            {
                TempData["ErrorMessage"] = "Cannot delete this user because they have related orders.";
                return RedirectToAction("GetAllUsers");
            }

            var result = await _userManager.DeleteAsync(user);
            TempData[(result.Succeeded ? "SuccessMessage" : "ErrorMessage")] =
                result.Succeeded ? $"User '{user.UserName}' deleted successfully."
                                 : string.Join(" | ", result.Errors.Select(e => e.Description));

            return RedirectToAction("GetAllUsers");
        }

    }
}
