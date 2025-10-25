using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;

namespace Project_Resturant_MVC.Services
{
    public class InventoryService
    {
        private readonly ResturantDbContext _context;
        private static DateTime _lastReset = DateTime.Today;
        private static readonly int DAILY_LIMIT = 50;

        public InventoryService(ResturantDbContext context)
        {
            _context = context;
        }

        // إجمالي الكمية المطلوبة اليوم لمنتج معيّن (يستثني الـ Cancelled/Deleted)
        public async Task<int> GetOrderedQtyTodayAsync(int menuItemId)
        {
            ResetIfNewDay();

            var today = DateTime.Today;

            return await _context.OrderItems
                .Where(oi => oi.MenuItemId == menuItemId &&
                             oi.Order.CreatedAt.Date == today &&
                             !oi.Order.IsDeleted &&
                             oi.Order.Status != OrderStatus.Cancelled)
                .SumAsync(oi => (int?)oi.Quantity) ?? 0;
        }

        // تُستدعى بعد إنشاء الأوردر عشان تقفل المنتج لو وصل للحد اليومي
        public async Task TrackOrderAsync(Order order)
        {
            ResetIfNewDay();

            foreach (var item in order.OrderItems)
            {
                var totalTodayAfterThisOrder = await GetOrderedQtyTodayAsync(item.MenuItemId);

                // لو وصل/عدّى الـ 50 بعد إضافة الأوردر => نقفل الـ Item
                if (totalTodayAfterThisOrder >= DAILY_LIMIT)
                {
                    var menuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == item.MenuItemId);
                    if (menuItem != null && menuItem.IsAvailable)
                    {
                        menuItem.IsAvailable = false;
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        private void ResetIfNewDay()
        {
            if (DateTime.Today > _lastReset)
            {
                _lastReset = DateTime.Today;

                // نفتح كل العناصر المقفولة لليوم الجديد
                var items = _context.MenuItems.Where(m => !m.IsAvailable);
                foreach (var m in items)
                    m.IsAvailable = true;

                _context.SaveChanges();
            }
        }

        public int GetDailyLimit() => DAILY_LIMIT;
    }
}
