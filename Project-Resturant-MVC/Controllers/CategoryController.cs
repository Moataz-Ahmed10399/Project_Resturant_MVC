using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;
using Project_Resturant_MVC.ViewModel;

namespace Project_Resturant_MVC.Controllers
{
    [Route("[controller]")]

    public class CategoryController : Controller
    {
        private readonly ResturantDbContext _Context;

        public CategoryController(ResturantDbContext context)
        {
            _Context = context;
        }
        [HttpGet("")]

        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategory()
        {
            var cat = await _Context.Categories.Where(c => c.MenuItems.Any(m => m.IsAvailable && !m.IsDeleted))

                    .Include(c => c.MenuItems).ToListAsync();

            return View(cat);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("create")]

        public async Task<IActionResult> Create ()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create (VmCategory vmcat )
        {
            if (!ModelState.IsValid)
            {
                return View(vmcat);
            }
            if (await _Context.Categories.AnyAsync(c=>c.Name==vmcat.Name ))
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");

                return View(vmcat);
            }

            var category = new Category
            {
                Name = vmcat.Name,
                Description = vmcat.Description,
            };

            await _Context.Categories.AddAsync(category);
            await _Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Category created successfully!";

            return RedirectToAction("GetAllCategory");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Update/{id:int}")]

        public async Task<IActionResult>Update(int id)
        {
            var cat = await _Context.Categories.FindAsync(id);
            if (cat == null) return NotFound();

            var vmcat = new VmCategory
            {
                Name = cat.Name, Description = cat.Description,
            };

            ViewBag.CatId = id;
            return View(vmcat);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost("Update/{id:int}")]
        public async Task<IActionResult> Update(int id, VmCategory vmcat)
        {
            if(!ModelState.IsValid)
            {
                return View(vmcat);
            }
            if (await _Context.Categories.AnyAsync(c=>c.Name==vmcat.Name && c.Id != id))
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");
                return View(vmcat);
            }

            var oldcat = await _Context.Categories.FindAsync(id);
            if (oldcat == null) return NotFound();

            oldcat.Name = vmcat.Name;
            oldcat.Description = vmcat.Description;
            oldcat.UpdatedAt = DateTime.Now;


            _Context.Categories.Update(oldcat);

            await _Context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Category updated successfully!";

            return RedirectToAction("GetAllCategory");

        }
        [AllowAnonymous]
        [HttpGet("Details/{id:int}")]

        public async Task<IActionResult> Details(int id)
        {
            var category = await _Context.Categories
                .Include(c => c.MenuItems) 
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category == null) return NotFound();

            return View(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Delete/{id:int}")]

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _Context.Categories.FindAsync(id);
            if (item == null) return NotFound();
            if (item.IsDeleted)
            {
                TempData["SuccessMessage"] = "Category already deleted.";
                return RedirectToAction("GetAllCategory");
            }
            item.IsDeleted = true;

            await _Context.SaveChangesAsync();
            return RedirectToAction("GetAllCategory");

        }
    }
}
