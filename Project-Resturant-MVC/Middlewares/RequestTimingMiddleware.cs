using System.Diagnostics;

namespace Project_Resturant_MVC.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            // ✅ اضمن إضافة الهيدر قبل إرسال الريسبونس
            context.Response.OnStarting(() =>
            {
                try
                {
                    // ممكن تستخدم Server-Timing كمان لو حابب
                    context.Response.Headers["X-Elapsed-Ms"] = sw.ElapsedMilliseconds.ToString();
                }
                catch
                {
                    // تجاهل أي أخطاء هنا
                }
                return Task.CompletedTask;
            });

            await _next(context);

            sw.Stop();

            // Logging بس بعد التشغيل
            _logger.LogInformation("HTTP {Method} {Path} -> {Status} in {Elapsed} ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                sw.ElapsedMilliseconds);
        }
    }

    public static class RequestTimingExtensions
    {
        public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app)
            => app.UseMiddleware<RequestTimingMiddleware>();
    }
}
