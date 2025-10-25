using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;
using Project_Resturant_MVC.ViewModel;

namespace Project_Resturant_MVC.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly ResturantDbContext _Context;

        public MenuItemController(ResturantDbContext context)
        {
            _Context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetAllMenuItem()
        {
            var items = await _Context.MenuItems
                .Include(m => m.Category) 
                //.Where(m => m.IsAvailable)
                .ToListAsync();
            
            return View(items);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var cat = await _Context.Categories.ToListAsync();

            VmMenuItem vmMenuItem = new VmMenuItem()
            {
                Categories = new SelectList(cat, "Id", "Name")

            };

            return View(vmMenuItem);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(VmMenuItem vmMenuItem)
        {
            if (!ModelState.IsValid)
            {
                //var cat = await _Context.Categories.ToListAsync();
                vmMenuItem.Categories = await GetAllCategories();  // new SelectList(cat, "Id", "Name");
                return View(vmMenuItem);
            }
            if (await _Context.MenuItems.AnyAsync(m => m.Name == vmMenuItem.Name))
            {
                ModelState.AddModelError("Name", "A MenuItems with this name already exists.");
                vmMenuItem.Categories = await GetAllCategories();
                return View(vmMenuItem);
            }

            var mitem = new MenuItem()
            {

                Name = vmMenuItem.Name,
                Description = vmMenuItem.Description,
                CategoryId = vmMenuItem.CategoryId,
                Price = vmMenuItem.Price,
                PreparationTimeMinutes = vmMenuItem.PreparationTimeMinutes,
                Quantity = vmMenuItem.Quantity
            };
                
            await _Context.MenuItems.AddAsync(mitem);
            await _Context.SaveChangesAsync();
            TempData["SuccessMessage"] = "MenuItem created successfully!";
            return RedirectToAction("GetAllMenuItem");


        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id)
        {
            var MenuItem = await _Context.MenuItems.FindAsync(id);
            var vmmenitem = new VmMenuItem
            {
                Categories = await GetAllCategories(),
                CategoryId=MenuItem.CategoryId,
                Description = MenuItem.Description,
                Price = MenuItem.Price,
                Name = MenuItem.Name,
                PreparationTimeMinutes=MenuItem.PreparationTimeMinutes,
                Quantity = MenuItem.Quantity

            };
            ViewBag.MenuItemId = id;

            return View(vmmenitem);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, VmMenuItem vmMenuItem)
        {
            if (!ModelState.IsValid)
            {
                vmMenuItem.Categories= await GetAllCategories();
                ViewBag.MenuItemId = id;
                return View(vmMenuItem);
            }
            if (await _Context.MenuItems.AnyAsync(c => c.Name == vmMenuItem.Name && c.Id != id))
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");
                vmMenuItem.Categories = await GetAllCategories();
                ViewBag.MenuItemId = id;

                return View(vmMenuItem);
            }
            var item = await _Context.MenuItems.FindAsync(id);

            item.Name = vmMenuItem.Name;
            item.Description = vmMenuItem.Description;
            item.Price = vmMenuItem.Price;
            item.Quantity = vmMenuItem.Quantity;
            item.PreparationTimeMinutes = vmMenuItem.PreparationTimeMinutes;
            item.CategoryId = vmMenuItem.CategoryId;
            
            await _Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Menu item updated successfully!";
            return RedirectToAction("GetAllMenuItem");
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteMenuItem(int id )
        {
            var item = await _Context.MenuItems.FindAsync(id);
            item.IsDeleted = true;

            await _Context.SaveChangesAsync();
            return RedirectToAction("GetAllMenuItem");

        }

        public async Task<SelectList> GetAllCategories()
        {
            var cat = await _Context.Categories.ToListAsync();
            return new SelectList(cat, "Id", "Name");

        }


    }
}
