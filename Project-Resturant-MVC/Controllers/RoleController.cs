using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Resturant_MVC.ViewModel;
using System.Threading.Tasks;

namespace Project_Resturant_MVC.Controllers
{
    [Authorize(Roles ="Admin")]                 //hena hyt2ked men al cookie an fe al key ally asmo role mwgod gwah admin
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleVm rvm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = rvm.RoleName;
              IdentityResult result= await roleManager.CreateAsync(role);
                if (result.Succeeded == true)
                { 
                    ViewBag.Success = true;
                    return View("AddRole");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(rvm);
        }
    }
}
