using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;
using Project_Resturant_MVC.Services;
using Project_Resturant_MVC.ViewModel;
using System.Security.Claims;

namespace Project_Resturant_MVC.Controllers
{
    [Authorize]
    [Route("[controller]")]

    public class OrderController : Controller
    {
        private readonly ResturantDbContext _Context;
        private readonly InventoryService _inventoryService;

        public OrderController(ResturantDbContext context, InventoryService inventoryService)
        {
            _Context = context;
            _inventoryService = inventoryService;
        }
        [HttpGet("")]

        //[Authorize(Roles = "Client")]
        public async Task<IActionResult> GetAllOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _Context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.UserId == userId && !o.IsDeleted)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return View(orders);
        }
        

        [HttpGet("create")]
        [Authorize(Roles = "Client")]

        public async Task<IActionResult> Create()
        {
            var menuItems = await _Context.MenuItems
                .Where(m => m.IsAvailable && !m.IsDeleted)
                .ToListAsync();

            var vm = new VmOrder
            {
                MenuItemsSelect = new SelectList(menuItems, "Id", "Name"),
                OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)))
            };

            vm.Items.Add(new VmOrderItem { Quantity = 1 });

            return View(vm);
        }

        [Authorize(Roles = "Client")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(VmOrder vmOrder)
        {
            if (vmOrder.Type == OrderType.Delivery && string.IsNullOrWhiteSpace(vmOrder.DeliveryAddress))
            {
                ModelState.AddModelError("DeliveryAddress", "Delivery address is required for delivery orders.");
            }

            if (!ModelState.IsValid)
            {
                var menuItems = await _Context.MenuItems.Where(m => m.IsAvailable).ToListAsync();
                vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
                vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));

                if (vmOrder.Items == null || !vmOrder.Items.Any())
                    vmOrder.Items.Add(new VmOrderItem { Quantity = 1 });

                return View(vmOrder);
            }

            //foreach (var item in vmOrder.Items)
            //{
            //    var menuItem = await _Context.MenuItems.FindAsync(item.MenuItemId);

            //    if (menuItem == null)
            //    {
            //        ModelState.AddModelError("", $"Menu item with ID {item.MenuItemId} not found.");
            //        return await ReloadCreateView(vmOrder);
            //    }

            //    if (!menuItem.IsAvailable)
            //    {
            //        ModelState.AddModelError("", $"{menuItem.Name} is currently unavailable.");
            //        return await ReloadCreateView(vmOrder);
            //    }

            //    if (menuItem.Quantity < item.Quantity)
            //    {
            //        ModelState.AddModelError("", $"Only {menuItem.Quantity} of {menuItem.Name} available.");
            //        return await ReloadCreateView(vmOrder);
            //    }
            //}
            foreach (var item in vmOrder.Items)
            {
                var menuItem = await _Context.MenuItems
                    .FirstOrDefaultAsync(m => m.Id == item.MenuItemId && !m.IsDeleted);

                if (menuItem == null)
                {
                    ModelState.AddModelError("", $"Menu item with ID {item.MenuItemId} not found.");
                    return await ReloadCreateView(vmOrder);
                }

                if (!menuItem.IsAvailable)
                {
                    ModelState.AddModelError("", $"{menuItem.Name} is currently unavailable.");
                    return await ReloadCreateView(vmOrder);
                }

                if (menuItem.Quantity < item.Quantity)
                {
                    ModelState.AddModelError("", $"Only {menuItem.Quantity} of {menuItem.Name} available in stock.");
                    return await ReloadCreateView(vmOrder);
                }

                var orderedToday = await _inventoryService.GetOrderedQtyTodayAsync(item.MenuItemId);
                var dailyLimit = _inventoryService.GetDailyLimit();
                var remaining = dailyLimit - orderedToday;

                if (remaining <= 0)
                {
                    ModelState.AddModelError("", $"'{menuItem.Name}' reached today's limit ({dailyLimit}). Please choose another item.");
                    return await ReloadCreateView(vmOrder);
                }

                if (item.Quantity > remaining)
                {
                    ModelState.AddModelError("", $"Only {remaining} of '{menuItem.Name}' available today (daily limit {dailyLimit}).");
                    return await ReloadCreateView(vmOrder);
                }
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = new Order
            {
                CustomerName = vmOrder.CustomerName,
                CustomerPhone = vmOrder.CustomerPhone,
                DeliveryAddress = vmOrder.DeliveryAddress,
                Status = OrderStatus.Pending,
                Type = vmOrder.Type,
                UserId = userId, 
                OrderItems = new List<OrderItem>()
            };

            decimal subtotal = 0;
            int maxPrepTime = 0;

            foreach (var item in vmOrder.Items)
            {
                var menuItem = await _Context.MenuItems.FindAsync(item.MenuItemId);

                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = menuItem.Price,
                    Subtotal = item.Quantity * menuItem.Price
                });

                subtotal += item.Quantity * menuItem.Price;
                menuItem.Quantity -= item.Quantity;

                if (menuItem.Quantity == 0)
                {
                    menuItem.IsAvailable = false;
                }

                if (menuItem.PreparationTimeMinutes > maxPrepTime)
                {
                    maxPrepTime = menuItem.PreparationTimeMinutes;
                }
            }

            order.Subtotal = subtotal;
            order.Tax = subtotal * 0.085m;
            order.Discount = 0;
            order.Total = order.Subtotal + order.Tax - order.Discount;
            order.EstimatedDeliveryMinutes = maxPrepTime + 30;

            await _Context.Orders.AddAsync(order);
            await _Context.SaveChangesAsync();

            await _inventoryService.TrackOrderAsync(order);

            TempData["SuccessMessage"] = $"Order #{order.Id} created successfully! Estimated delivery: {order.EstimatedDeliveryMinutes} minutes.";
            return RedirectToAction("GetAllOrders");
        }
        [HttpGet("update/{id:int}")]

        public async Task<IActionResult> Update(int id)
        {
            var order = await _Context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);

            if (order == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!User.IsInRole("Admin") && order.UserId != userId)
            {
                TempData["ErrorMessage"] = "You can only edit your own orders.";
                return RedirectToAction("GetAllOrders");
            }

            if (order.Status == OrderStatus.Ready || order.Status == OrderStatus.Delivered)
            {
                TempData["ErrorMessage"] = "Cannot edit orders that are Ready or Delivered.";
                return RedirectToAction("GetAllOrders");
            }

            var menuItems = await _Context.MenuItems
                .Where(m => m.IsAvailable && !m.IsDeleted)
                .ToListAsync();

            var vm = new VmOrder
            {
                CustomerName = order.CustomerName,
                CustomerPhone = order.CustomerPhone,
                DeliveryAddress = order.DeliveryAddress,
                Type = order.Type,
                Items = order.OrderItems.Select(oi => new VmOrderItem
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity
                }).ToList(),
                MenuItemsSelect = new SelectList(menuItems, "Id", "Name"),
                OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)))
            };
            ViewBag.CurrentStatus = order.Status;
            ViewBag.OrderId = id;
            return View(vm);
        }

        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> Update(int id, VmOrder vmOrder)
        {
            var order = await _Context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);

            if (order == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!User.IsInRole("Admin") && order.UserId != userId)
            {
                TempData["ErrorMessage"] = "You can only edit your own orders.";
                return RedirectToAction("GetAllOrders");
            }

            if (order.Status == OrderStatus.Ready || order.Status == OrderStatus.Delivered)
            {
                TempData["ErrorMessage"] = "Cannot edit orders that are Ready or Delivered.";
                return RedirectToAction("GetAllOrders");
            }

            if (vmOrder.Type == OrderType.Delivery && string.IsNullOrWhiteSpace(vmOrder.DeliveryAddress))
            {
                ModelState.AddModelError("DeliveryAddress", "Delivery address is required for delivery orders.");
            }

            if (vmOrder.Items == null || !vmOrder.Items.Any())
                ModelState.AddModelError("", "Order must contain at least one item.");

            if (!ModelState.IsValid)
            {
                var menuItems = await _Context.MenuItems.Where(m => m.IsAvailable && !m.IsDeleted).ToListAsync();
                vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
                vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));
                ViewBag.OrderId = id;
                ViewBag.CurrentStatus = order.Status;

                if (vmOrder.Items == null || !vmOrder.Items.Any())
                    vmOrder.Items = new List<VmOrderItem> { new VmOrderItem { Quantity = 1 } };

                return View(vmOrder);
            }
            //using var tx = await _Context.Database.BeginTransactionAsync();

            //foreach (var oldItem in order.OrderItems)
            //{
            //    var menuItem = await _Context.MenuItems.FindAsync(oldItem.MenuItemId);
            //    menuItem.Quantity += oldItem.Quantity;
            //    menuItem.IsAvailable = true;
            //}
            foreach (var oldItem in order.OrderItems)
            {
                var oldMenu = await _Context.MenuItems.FindAsync(oldItem.MenuItemId);
                if (oldMenu != null)
                {
                    oldMenu.Quantity += oldItem.Quantity;
                    if (oldMenu.Quantity > 0) oldMenu.IsAvailable = true;
                }
            }

            order.CustomerName = vmOrder.CustomerName;
            order.CustomerPhone = vmOrder.CustomerPhone;
            order.DeliveryAddress = vmOrder.DeliveryAddress;
            order.Type = vmOrder.Type;

            order.OrderItems.Clear();
            

            decimal subtotal = 0;
            int maxPrepTime = 0;

            foreach (var item in vmOrder.Items)
            {
                var menuItem = await _Context.MenuItems
                    .FirstOrDefaultAsync(m => m.Id == item.MenuItemId && !m.IsDeleted);

                //if (!menuItem.IsAvailable || menuItem.Quantity < item.Quantity)
                if (menuItem == null || !menuItem.IsAvailable || menuItem.Quantity < item.Quantity)
                {
                    //ModelState.AddModelError("", $"{menuItem?.Name ?? "Item"} is unavailable or insufficient quantity.");
                    //ViewBag.OrderId = id;
                    ////await tx.RollbackAsync();

                    //return await ReloadUpdateView(vmOrder, id);
                    ModelState.AddModelError("", $"{menuItem?.Name ?? "Item"} is unavailable or insufficient quantity.");
                    var menuItems = await _Context.MenuItems.Where(m => m.IsAvailable && !m.IsDeleted).ToListAsync();
                    vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
                    vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));
                    ViewBag.OrderId = id;
                    ViewBag.CurrentStatus = order.Status;
                    return View(vmOrder);
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                var orderedToday = await _inventoryService.GetOrderedQtyTodayAsync(item.MenuItemId);
                var dailyLimit = _inventoryService.GetDailyLimit();
                var remaining = dailyLimit - orderedToday;

                if (remaining <= 0)
                {
                    ModelState.AddModelError("", $"'{menuItem.Name}' reached today's limit ({dailyLimit}).");
                    var menuItems = await _Context.MenuItems.Where(m => m.IsAvailable && !m.IsDeleted).ToListAsync();
                    vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
                    vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));
                    ViewBag.OrderId = id;
                    ViewBag.CurrentStatus = order.Status;
                    return View(vmOrder);
                }

                if (item.Quantity > remaining)
                {
                    ModelState.AddModelError("", $"Only {remaining} of '{menuItem.Name}' available today (daily limit {dailyLimit}).");
                    var menuItems = await _Context.MenuItems.Where(m => m.IsAvailable && !m.IsDeleted).ToListAsync();
                    vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
                    vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));
                    ViewBag.OrderId = id;
                    ViewBag.CurrentStatus = order.Status;
                    return View(vmOrder);
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = menuItem.Price,
                    Subtotal = item.Quantity * menuItem.Price
                });

                subtotal += item.Quantity * menuItem.Price;
                menuItem.Quantity -= item.Quantity;

                if (menuItem.Quantity <= 0)
                {
                    menuItem.Quantity = 0;
                    menuItem.IsAvailable = false;
                }

                maxPrepTime = Math.Max(maxPrepTime, menuItem.PreparationTimeMinutes);

            }

            order.Subtotal = subtotal;
            order.Tax = subtotal * 0.085m;
            order.Discount = order.Discount;
            order.Total = order.Subtotal + order.Tax - order.Discount;
            order.EstimatedDeliveryMinutes = maxPrepTime + 30;

            await _Context.SaveChangesAsync();
            await _inventoryService.TrackOrderAsync(order);

            TempData["SuccessMessage"] = "Order updated successfully!";
            return RedirectToAction("GetAllOrders");
        }

        [HttpGet("updatestatus/{id:int}")]

        [Authorize(Roles = "Kitchen")] 
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var order = await _Context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost("updatestatus/{id:int}")]
        [Authorize(Roles = "Kitchen")] 
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus newStatus)
        {
            var order = await _Context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            if (newStatus < order.Status)
            {
                TempData["ErrorMessage"] = "Cannot move order backwards in status.";
                return RedirectToAction("KitchenDashboard");
            }

            if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
            {
                TempData["ErrorMessage"] = "Cannot update completed or cancelled orders.";
                return RedirectToAction("KitchenDashboard");
            }

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;

            if (newStatus == OrderStatus.Ready && order.ReadyAt == null)
            {
                order.ReadyAt = DateTime.UtcNow;
            }

            if (newStatus == OrderStatus.Delivered && order.DeliveredAt == null)
            {
                order.DeliveredAt = DateTime.UtcNow;
            }

            await _Context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Order #{id} status updated to {newStatus}";
            return RedirectToAction("KitchenDashboard");
        }

        [HttpGet("kitchendashboard")]

        [Authorize(Roles = "Kitchen")] 
        public async Task<IActionResult> KitchenDashboard()
        {
            var orders = await _Context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.Status != OrderStatus.Delivered &&
                            o.Status != OrderStatus.Cancelled &&
                            !o.IsDeleted)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();

            return View(orders);
        }

        [HttpGet("adminordersdashboard")]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminOrdersDashboard()
        {
            var orders = await _Context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Include(o => o.User)
                .Where(o => !o.IsDeleted)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            ViewBag.PendingCount = orders.Count(o => o.Status == OrderStatus.Pending);
            ViewBag.PreparingCount = orders.Count(o => o.Status == OrderStatus.Preparing);
            ViewBag.ReadyCount = orders.Count(o => o.Status == OrderStatus.Ready);
            ViewBag.DeliveredCount = orders.Count(o => o.Status == OrderStatus.Delivered);
            ViewBag.TotalRevenue = orders.Where(o => o.Status == OrderStatus.Delivered)
                                          .Sum(o => o.Total);

            return View(orders);
        }

        [HttpGet("Details/{id:int}")]

        public async Task<IActionResult> Details(int id)
        {
            var order = await _Context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!User.IsInRole("Admin") && order.UserId != userId)
            {
                TempData["ErrorMessage"] = "You can only view your own orders.";
                return RedirectToAction("GetAllOrders");
            }

            return View(order);
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _Context.Orders
                .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);

            if (order == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!User.IsInRole("Admin") && order.UserId != userId)
            {
                TempData["ErrorMessage"] = "You can only delete your own orders.";
                return RedirectToAction("GetAllOrders");
            }

            if (order.Status == OrderStatus.Ready || order.Status == OrderStatus.Delivered)
            {
                TempData["ErrorMessage"] = "Cannot delete orders that are Ready or Delivered.";
                return RedirectToAction("GetAllOrders");
            }

            foreach (var item in order.OrderItems)
            {
                var menuItem = await _Context.MenuItems.FindAsync(item.MenuItemId);
                menuItem.Quantity += item.Quantity;
                menuItem.IsAvailable = true;
            }

            order.IsDeleted = true;
            await _Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order cancelled and stock restored.";
            return RedirectToAction("GetAllOrders");
        }

    
        private async Task<IActionResult> ReloadCreateView(VmOrder vmOrder)
        {
            var menuItems = await _Context.MenuItems.Where(m => m.IsAvailable && !m.IsDeleted).ToListAsync();
            vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
            vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));
            return View("Create", vmOrder);
        }

        private async Task<IActionResult> ReloadUpdateView(VmOrder vmOrder, int orderId)
        {
            var menuItems = await _Context.MenuItems.Where(m => m.IsAvailable && !m.IsDeleted).ToListAsync();
            vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
            vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));
            ViewBag.OrderId = orderId;
            return View("Update", vmOrder);
        }
    }
}