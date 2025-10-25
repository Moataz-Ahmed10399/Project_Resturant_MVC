using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;

namespace Project_Resturant_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AnalyticsController : Controller
    {
        private readonly ResturantDbContext _context;

        public AnalyticsController(ResturantDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var today = DateTime.Now.Date;
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                 .ThenInclude(i => i.MenuItem)
                .Where(o => !o.IsDeleted)
                .ToListAsync();

            var totalSales = orders.Sum(o => o.Total);
            var totalOrders = orders.Count;
            var avgOrder = totalOrders > 0 ? totalSales / totalOrders : 0;

            // sales per day (آخر 7 أيام)
            var last7DaysSales = orders
                .Where(o => o.CreatedAt >= today.AddDays(-6))
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new { Date = g.Key, Total = g.Sum(x => x.Total) })
                .OrderBy(g => g.Date)
                .ToList();

            // top 5 items
            var topItems = orders
                .SelectMany(o => o.OrderItems)
                .Where(i => i.MenuItem != null)
                .GroupBy(i => i.MenuItem.Name)
                .Select(g => new { Name = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToList();

            ViewBag.TotalSales = totalSales;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.AvgOrder = avgOrder;
            ViewBag.Last7DaysSales = last7DaysSales;
            ViewBag.TopItems = topItems;

            return View();
        }
    }
}
