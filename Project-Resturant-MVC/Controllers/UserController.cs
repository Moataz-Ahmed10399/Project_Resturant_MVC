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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ResturantDbContext _db;  
        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,ResturantDbContext db)          // <-- استقبله
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;                                        
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


        [HttpPost("changerole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
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

            // لو غيّرت دورك وإنت اللوجد إن كأدمن
            var me = _userManager.GetUserId(User);
            if (user.Id == me && !string.Equals(newRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                TempData["SuccessMessage"] = $"Your role is now '{newRole}'. You were signed out.";
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            TempData["SuccessMessage"] = $"User '{user.UserName}' role changed to '{newRole}'.";
            return RedirectToAction("GetAllUsers");
        }


        [HttpPost("deleteuser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId, string newRole)
        {
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //    return NotFound();

            //if (user.UserName == "admin")
            //{
            //    TempData["ErrorMessage"] = "Cannot delete the main admin account.";
            //    return RedirectToAction("GetAllUsers");
            //}

            //await _userManager.DeleteAsync(user);
            //TempData["SuccessMessage"] = $"User '{user.UserName}' deleted successfully.";
            //return RedirectToAction("GetAllUsers");
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

            var currentUserId = _userManager.GetUserId(User);
            if (user.Id == currentUserId && !string.Equals(newRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                TempData["SuccessMessage"] = $"Your role is now '{newRole}'. You were signed out.";
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            TempData["SuccessMessage"] = $"User '{user.UserName}' role changed to '{newRole}'.";
            return RedirectToAction("GetAllUsers");
        }
    }
}
