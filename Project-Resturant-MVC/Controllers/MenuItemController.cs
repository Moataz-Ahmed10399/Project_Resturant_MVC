using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;
using Project_Resturant_MVC.Services;
using Project_Resturant_MVC.ViewModel;

namespace Project_Resturant_MVC.Controllers
{
    [Route("[controller]")]

    public class MenuItemController : Controller
    {
        private readonly ResturantDbContext _Context;
        private readonly IFileStorageService _fileService;

        public MenuItemController(ResturantDbContext context , IFileStorageService fileService)
        {
            _Context = context;
            _fileService = fileService;

        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> GetAllMenuItem()
        {
            var items = await _Context.MenuItems
                .Include(m => m.Category)
                .Where(m => !m.IsDeleted)
                .ToListAsync();
            
            return View(items);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var cat = await _Context.Categories.Where(c => !c.IsDeleted).ToListAsync();

            VmMenuItem vmMenuItem = new VmMenuItem()
            {
                Categories = new SelectList(cat, "Id", "Name")

            };

            return View(vmMenuItem);
        }
        [HttpPost("create")]
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
                Quantity = vmMenuItem.Quantity,
                 IsAvailable = vmMenuItem.Quantity > 0
            };
            if (vmMenuItem.ImageFile != null)
            {
                try
                {
                    mitem.ImagePath = await _fileService.SaveMenuImageAsync(vmMenuItem.ImageFile);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ImageFile", ex.Message);
                    vmMenuItem.Categories = await GetAllCategories();
                    return View(vmMenuItem);
                }
            }
            else
            {
                mitem.ImagePath = "images/menu/default.png";
            }


            await _Context.MenuItems.AddAsync(mitem);
            await _Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "MenuItem created successfully!";
            return RedirectToAction("GetAllMenuItem");


        }

        [Authorize(Roles = "Admin")]
        [HttpGet("update/{id:int}")]

        public async Task<IActionResult> Update(int id)
        {
            var MenuItem = await _Context.MenuItems.FindAsync(id);
            if (MenuItem == null || MenuItem.IsDeleted) return NotFound();

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

        [HttpPost("update/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, VmMenuItem vmMenuItem)
        {


            var item = await _Context.MenuItems.FindAsync(id);
            if (item == null || item.IsDeleted) return NotFound();


            if (!ModelState.IsValid)
            {
                vmMenuItem.Categories= await GetAllCategories();
                ViewBag.MenuItemId = id;
                return View(vmMenuItem);
            }
            if (await _Context.MenuItems.AnyAsync(c => c.Name == vmMenuItem.Name && c.Id != id))
            {
                ModelState.AddModelError("Name", "A menu item with this name already exists.");
                vmMenuItem.Categories = await GetAllCategories();
                ViewBag.MenuItemId = id;

                return View(vmMenuItem);
            }

            item.Name = vmMenuItem.Name;
            item.Description = vmMenuItem.Description;
            item.Price = vmMenuItem.Price;
            item.Quantity = vmMenuItem.Quantity;
            item.PreparationTimeMinutes = vmMenuItem.PreparationTimeMinutes;
            item.CategoryId = vmMenuItem.CategoryId;
            item.IsAvailable = item.Quantity > 0;

            if (vmMenuItem.ImageFile != null)
            {
                try
                {
                    var newPath = await _fileService.SaveMenuImageAsync(vmMenuItem.ImageFile);
                    if (!string.IsNullOrWhiteSpace(item.ImagePath) && !item.ImagePath.EndsWith("default.png"))
                        await _fileService.DeleteAsync(item.ImagePath);

                    item.ImagePath = newPath;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ImageFile", ex.Message);
                    vmMenuItem.Categories = await GetAllCategories();
                    ViewBag.MenuItemId = id;
                    return View(vmMenuItem);
                }
            }

            await _Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Menu item updated successfully!";
            return RedirectToAction("GetAllMenuItem");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deletemenuitem/{id:int}")]
        public async Task<IActionResult> DeleteMenuItem(int id )
        {
            var item = await _Context.MenuItems.FindAsync(id);
            if (item == null) return NotFound();

            item.IsDeleted = true;

            await _Context.SaveChangesAsync();
            return RedirectToAction("GetAllMenuItem");

        }

        public async Task<SelectList> GetAllCategories()
        {
            var cat = await _Context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            return new SelectList(cat, "Id", "Name");

        }


    }
}
