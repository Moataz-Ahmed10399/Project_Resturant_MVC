using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Resturant_MVC.ViewModel;
using System.Threading.Tasks;

namespace Project_Resturant_MVC.Controllers
{
    [Authorize(Roles = "Admin")]                 //hena hyt2ked men al cookie an fe al key ally asmo role mwgod gwah admin
    [Route("[controller]")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        //[HttpGet("create")]
        [HttpGet("addrole")]
        public IActionResult AddRole() => View();

        [HttpPost("addrole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleVm rvm)
        {
            if (!ModelState.IsValid) return View(rvm);

            if (await roleManager.RoleExistsAsync(rvm.RoleName))
            {
                ModelState.AddModelError("", $"Role '{rvm.RoleName}' already exists.");
                return View(rvm);
            }

            var result = await roleManager.CreateAsync(new IdentityRole(rvm.RoleName));
            if (result.Succeeded)
            {
                ViewBag.Success = true;
                return View("AddRole");
            }

            foreach (var e in result.Errors)
                ModelState.AddModelError("", e.Description);

            return View(rvm);
        }
    }
}
