using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Resturant_MVC.Models;
using Project_Resturant_MVC.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Resturant_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register( RegisterUserViewModel uservm)
        {
            if (ModelState.IsValid)
            {
                //mapping 
                ApplicationUser appuser = new ApplicationUser()
                {
                    UserName = uservm.UserName,
                    Address = uservm.Address,
                    PasswordHash = uservm.Password
                };
                //saving in database

              IdentityResult result =  await userManager.CreateAsync(appuser ,uservm.Password);

                if (result.Succeeded) 
                {
                    //assign to role
                    //await userManager.AddToRoleAsync(appuser, "Admin");
                    await userManager.AddToRoleAsync(appuser, "Client");

                    await signInManager.SignInAsync(appuser, false);
                    return RedirectToAction("Index", "Home");

                    //cokkiee



                    //SignInManager<ApplicationUser> sign = new SignInManager<ApplicationUser>()
                    //await   signInManager.SignInAsync(appuser , false);
                    //return RedirectToAction("GetAllMenuItem", "MenuItem");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }

            return View(uservm);
        }
   
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  //بتدور علي ال __RequestVerificationToken لو لقيه مظبوط يدخله لو غلط يفكلسه عشان الحماية من ان حد من الكلاينت يتسرق 
        //لاوم تكون عند البوست طبعا 
        public async Task<IActionResult> Login( LoginUserViewModel loginvm)
        {
            if (ModelState.IsValid)
            {
                //check found
              ApplicationUser appuser = await userManager.FindByNameAsync(loginvm.UserName);

                if (appuser != null) 
                {
                 bool found = await userManager.CheckPasswordAsync(appuser, loginvm.Password);

                    if (found == true) 
                    {
                        List<Claim>claims = new List<Claim>();
                        claims.Add(new Claim("UserAddress", appuser.Address));
                         await signInManager.SignInWithClaimsAsync(appuser, loginvm.RememberMe,claims);

                        //return RedirectToAction("GetAllMenuItem", "MenuItem");
                        var roles = await userManager.GetRolesAsync(appuser);
                        var userRole = roles.FirstOrDefault();

                        if (userRole == "Kitchen")
                        {
                            return RedirectToAction("KitchenDashboard", "Order");
                        }
                        else if (userRole == "Admin")
                        {
                            return RedirectToAction("AdminOrdersDashboard", "Order");
                        }
                        else 
                        {
                            return RedirectToAction("GetAllMenuItem", "MenuItem");
                        }
                    }
                }
                ModelState.AddModelError("", "UserName OR Password is wrong");
                // createcookie
            
            }
            return View();
        }
         public async Task<IActionResult> SignOut()
        {
           await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home"); 

        }
    }
}
