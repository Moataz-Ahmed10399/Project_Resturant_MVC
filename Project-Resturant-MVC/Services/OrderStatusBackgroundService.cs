using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;

namespace Project_Resturant_MVC.Services
{
    public class OrderStatusBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderStatusBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ResturantDbContext>();

                    var nowUtc = DateTime.UtcNow;

                    // 1) Pending -> Preparing (بعد 5 دقائق من الانشاء)
                    var pendings = await db.Orders
                        .Where(o => !o.IsDeleted && o.Status == OrderStatus.Pending)
                        .ToListAsync(stoppingToken);

                    foreach (var o in pendings)
                    {
                        // CreatedAt assumed local or UTC؟ في مشروعك بتسجله default؟ هنعتبره UTC-safe:
                        var created = o.CreatedAt.Kind == DateTimeKind.Unspecified
                                      ? DateTime.SpecifyKind(o.CreatedAt, DateTimeKind.Utc)
                                      : o.CreatedAt.ToUniversalTime();

                        if (nowUtc - created >= TimeSpan.FromMinutes(5))
                        {
                            o.Status = OrderStatus.Preparing;
                            o.PreparingAt = nowUtc;
                            o.UpdatedAt = nowUtc;
                        }
                    }

                    // 2) Preparing -> Ready (بعد أقصى وقت تحضير لعناصر الطلب)
                    var preparing = await db.Orders
                        .Include(x => x.OrderItems)
                            .ThenInclude(oi => oi.MenuItem)
                        .Where(o => !o.IsDeleted && o.Status == OrderStatus.Preparing)
                        .ToListAsync(stoppingToken);

                    foreach (var o in preparing)
                    {
                        var prepStart = o.PreparingAt ?? o.UpdatedAt ?? o.CreatedAt;
                        var prepStartUtc = prepStart.Kind == DateTimeKind.Unspecified
                                            ? DateTime.SpecifyKind(prepStart, DateTimeKind.Utc)
                                            : prepStart.ToUniversalTime();

                        var maxPrep = o.OrderItems?.Any() == true
                            ? o.OrderItems.Max(i => i.MenuItem?.PreparationTimeMinutes ?? 0)
                            : 0;

                        if (maxPrep <= 0) maxPrep = 10; // fallback منطقي

                        if (nowUtc - prepStartUtc >= TimeSpan.FromMinutes(maxPrep))
                        {
                            o.Status = OrderStatus.Ready;
                            o.ReadyAt ??= nowUtc;
                            o.UpdatedAt = nowUtc;
                        }
                    }

                    await db.SaveChangesAsync(stoppingToken);
                }
                catch
                {
                    // ممكن تسجّل لوج لو حبيت
                }

                // انتظر 30 ثانية بين كل دورة
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
