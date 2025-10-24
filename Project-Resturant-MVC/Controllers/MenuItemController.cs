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
        private readonly IWebHostEnvironment _webHostEnvironment; 

        public MenuItemController(ResturantDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                    return null;

                // اسم الصورة فريد
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                // المسار الكامل للصورة
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "menuitems");

                // تأكد إن المجلد موجود
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, fileName);

                // احفظ الصورة
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // ارجع المسار النسبي
                return "/images/menuitems/" + fileName;
            }
            catch (Exception ex)
            {
                // ✅ لو حصل error، اطبعه في Console
                Console.WriteLine($"Error saving image: {ex.Message}");
                return null;
            }
        }

        private void DeleteImage(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    return;

                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('/'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting image: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetAllMenuItem()
        {
            var items = await _Context.MenuItems
                .Include(m => m.Category) // علشان تجيب اسم الكاتيجوري كمان
                .ToListAsync();
            
            return View(items);
        }

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

        public async Task<IActionResult> Create(VmMenuItem vmMenuItem)
        {
            if (vmMenuItem.ImageFile == null || vmMenuItem.ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please upload an image for the menu item.");
            }

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


            try
            {
                // ✅ احفظ الصورة
                string imageUrl = await SaveImageAsync(vmMenuItem.ImageFile);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    ModelState.AddModelError("ImageFile", "Failed to upload image. Please try again.");
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
                    ImageUrl = imageUrl
                };

                await _Context.MenuItems.AddAsync(mitem);
                await _Context.SaveChangesAsync();

                TempData["SuccessMessage"] = "MenuItem created successfully!";
                return RedirectToAction("GetAllMenuItem");
            }
            catch (Exception ex)
            {
                // ✅ لو حصل أي error
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                vmMenuItem.Categories = await GetAllCategories();
                return View(vmMenuItem);
            }

        }


        public async Task<IActionResult> Update(int id)
        {
            var MenuItem = await _Context.MenuItems.FindAsync(id);
            if (MenuItem == null)
            {
                return NotFound();
            }
            var vmmenitem = new VmMenuItem
            {
                Categories = await GetAllCategories(),
                CategoryId=MenuItem.CategoryId,
                Description = MenuItem.Description,
                Price = MenuItem.Price,
                Name = MenuItem.Name,
                PreparationTimeMinutes=MenuItem.PreparationTimeMinutes,
                Quantity = MenuItem.Quantity,
                ImageUrl = MenuItem.ImageUrl
            };
            ViewBag.MenuItemId = id;

            return View(vmmenitem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, VmMenuItem vmMenuItem)
        {
            // ✅ شيل الـ ImageFile من الـ ModelState validation
            ModelState.Remove("ImageFile");

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


            //var item = await _Context.MenuItems.FindAsync(id);

            //item.Name = vmMenuItem.Name;
            //item.Description = vmMenuItem.Description;
            //item.Price = vmMenuItem.Price;
            //item.Quantity = vmMenuItem.Quantity;
            //item.PreparationTimeMinutes = vmMenuItem.PreparationTimeMinutes;
            //item.CategoryId = vmMenuItem.CategoryId;
            //if (vmMenuItem.ImageFile != null && vmMenuItem.ImageFile.Length > 0)
            //{
            //    DeleteImage(item.ImageUrl);

            //    item.ImageUrl = await SaveImageAsync(vmMenuItem.ImageFile);
            //}

            //await _Context.SaveChangesAsync();

            //TempData["SuccessMessage"] = "Menu item updated successfully!";
            //return RedirectToAction("GetAllMenuItem");
            try
            {
                var item = await _Context.MenuItems.FindAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                if (vmMenuItem.ImageFile != null && vmMenuItem.ImageFile.Length > 0)
                {
                    DeleteImage(item.ImageUrl);

                    string newImageUrl = await SaveImageAsync(vmMenuItem.ImageFile);

                    if (!string.IsNullOrEmpty(newImageUrl))
                    {
                        item.ImageUrl = newImageUrl;
                    }
                }

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
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                vmMenuItem.Categories = await GetAllCategories();
                ViewBag.MenuItemId = id;
                return View(vmMenuItem);
            }


        }


        public async Task<IActionResult> DeleteMenuItem(int id )
        {
            //var item = await _Context.MenuItems.FindAsync(id);
            //item.IsDeleted = true;


            //DeleteImage(item.ImageUrl);



            //_Context.SaveChangesAsync();
            //return RedirectToAction("GetAllMenuItem");

            try
            {
                var item = await _Context.MenuItems.FindAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                DeleteImage(item.ImageUrl);

                item.IsDeleted = true;
                await _Context.SaveChangesAsync();  // ✅ كان مكتوب _Context.SaveChangesAsync() من غير await

                TempData["SuccessMessage"] = "Menu item deleted successfully!";
                return RedirectToAction("GetAllMenuItem");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting item: {ex.Message}";
                return RedirectToAction("GetAllMenuItem");
            }

        }

        public async Task<SelectList> GetAllCategories()
        {
            var cat = await _Context.Categories.ToListAsync();
            return new SelectList(cat, "Id", "Name");

        }


    }
}
