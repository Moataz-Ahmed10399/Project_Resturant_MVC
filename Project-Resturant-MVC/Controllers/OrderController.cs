using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;
using Project_Resturant_MVC.ViewModel;

namespace Project_Resturant_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ResturantDbContext _Context;

        public OrderController(ResturantDbContext context)
        {
            //_Context = new ResturantDbContext();   // ليه هنا معملنها في ال براميتر 
            _Context = context;
        }

        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _Context.Orders
                .Include(O=>O.OrderItems)
                .ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            var menuItems = await _Context.MenuItems.ToListAsync();

            var vm = new VmOrder
            {
                MenuItemsSelect = new SelectList(menuItems, "Id", "Name"),
                OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)))
            };

            vm.Items.Add(new VmOrderItem { Quantity = 1 });


            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Create(VmOrder vmOrder)
        {
            if (!ModelState.IsValid)
            {
                var menuItems = await _Context.MenuItems.ToListAsync();
                vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
                vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));

                // إذا Items فارغة أو null، أضف عنصر واحد على الأقل
                if (vmOrder.Items == null || !vmOrder.Items.Any())
                    vmOrder.Items.Add(new VmOrderItem { Quantity = 1 });

                return View(vmOrder);
            }

            var order = new Order
            {
                CustomerName = vmOrder.CustomerName,
                CustomerPhone = vmOrder.CustomerPhone,
                DeliveryAddress = vmOrder.DeliveryAddress,
                Status = OrderStatus.Pending,
                Type = vmOrder.Type,
                Subtotal = 0,
                Tax = 0,
                Discount = 0,
                Total = 0,
                OrderItems = new List<OrderItem>() // << مهم جداً
            };

            foreach (var item in vmOrder.Items)
            {
                var menuItem = await _Context.MenuItems.FindAsync(item.MenuItemId);
                //if (menuItem == null) continue; 

                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = menuItem.Price,
                    Subtotal = item.Quantity * menuItem.Price
                });
            }

            order.Subtotal = order.OrderItems.Sum(i => i.Subtotal);
            order.Total = order.Subtotal + order.Tax - order.Discount;

            await _Context.Orders.AddAsync(order);
            await _Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order created successfully!";
            return RedirectToAction("GetAllOrders");
        }

        public async Task<IActionResult> Update(int id)
        {
            var order = await _Context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            var menuItems = await _Context.MenuItems.ToListAsync();

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
            ViewBag.OrderId = id;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, VmOrder vmOrder)
        {
            if (!ModelState.IsValid)
            {
                var menuItems = await _Context.MenuItems.ToListAsync();
                vmOrder.MenuItemsSelect = new SelectList(menuItems, "Id", "Name");
                vmOrder.OrderTypes = new SelectList(Enum.GetValues(typeof(OrderType)));
                ViewBag.OrderId = id;

                // لو الـ Items مش موجودة أو فاضية، أرجع عنصر واحد على الأقل
                if (vmOrder.Items == null || !vmOrder.Items.Any())
                    vmOrder.Items = new List<VmOrderItem> { new VmOrderItem { Quantity = 1 } };

                return View(vmOrder);
            }

            var order = await _Context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            order.CustomerName = vmOrder.CustomerName;
            order.CustomerPhone = vmOrder.CustomerPhone;
            order.DeliveryAddress = vmOrder.DeliveryAddress;
            order.Type = vmOrder.Type;

            // تحديث الـ OrderItems
            order.OrderItems.Clear();
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
            }

            order.Subtotal = order.OrderItems.Sum(i => i.Subtotal);
            order.Total = order.Subtotal + order.Tax - order.Discount;

            await _Context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order updated successfully!";
            return RedirectToAction("GetAllOrders");
        }



        public async Task<IActionResult> Details(int id)
        {
            // جلب الطلب مع العناصر المرتبطة به
            var order = await _Context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem) // لو عايزين نجيب اسم المنيو آيتم لكل OrderItem
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var orderdeleted = await _Context.Orders.FindAsync(id);
            orderdeleted.IsDeleted = true;

            await _Context.SaveChangesAsync();
            return RedirectToAction("GetAllOrders");
        }

       



    }
}
