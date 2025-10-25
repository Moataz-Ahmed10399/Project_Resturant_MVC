using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Resturant_MVC.Middlewares
{
    public class BusinessHoursOptions
    {
        public int StartHour { get; set; } = 9;     // 9 AM
        public int EndHour { get; set; } = 23;    // 11 PM
        public string ClosedMessage { get; set; } =
            "🚪 Restaurant is closed now. Working hours: {0}:00 - {1}:00";
    }

    public class BusinessHoursMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly BusinessHoursOptions _options;

        private static readonly string[] _publicPaths = new[]
        {
            "/", "/home", "/home/index", "/account/login", "/account/register", "/home/privacy"
        };

        public BusinessHoursMiddleware(RequestDelegate next, BusinessHoursOptions options)
        {
            _next = next;
            _options = options ?? new BusinessHoursOptions();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = (context.Request.Path.Value ?? "").ToLower();
            var method = context.Request.Method.ToUpperInvariant();

            if (IsStaticFile(path)) { await _next(context); return; }

            if (path.StartsWith("/closed")) { await _next(context); return; }

            if (IsPublicPath(path)) { await _next(context); return; }

            if (context.User?.Identity?.IsAuthenticated == true &&
               (context.User.IsInRole("Admin") || context.User.IsInRole("Kitchen")))
            {
                await _next(context);
                return;
            }

            var nowHour = DateTime.Now.Hour;
            if (!IsOpen(nowHour, _options.StartHour, _options.EndHour))
            {
                if (IsClientOrderMutation(path, method))
                {
                    var returnUrl = context.Request.Path + context.Request.QueryString;
                    var url = $"/closed?start={_options.StartHour}&end={_options.EndHour}&returnUrl={Uri.EscapeDataString(returnUrl)}";
                    context.Response.Redirect(url);
                    return;
                }
            }

            await _next(context);
        }

        private static bool IsOpen(int now, int start, int end)
        {
            if (start < end) return now >= start && now < end; // same-day window
            return now >= start || now < end;                   // spans midnight
        }

     
        private static bool IsClientOrderMutation(string path, string method)
        {
            if (!path.StartsWith("/order")) return false;

            if (path.Contains("/create")) return true;

            if (path.Contains("/update/")) return true;

            if (path.Contains("/delete") && method == "POST") return true;

            return false;
        }

        private static bool IsPublicPath(string path)
        {
            var p = path.TrimEnd('/');
            return _publicPaths.Contains(p);
        }

        private static bool IsStaticFile(string path)
        {
            return path.StartsWith("/css/") || path.StartsWith("/js/") || path.StartsWith("/lib/") ||
                   path.StartsWith("/images/") || path.StartsWith("/img/") || path.StartsWith("/favicon");
        }
    }

    public static class BusinessHoursMiddlewareExtensions
    {
        public static IApplicationBuilder UseBusinessHours(
            this IApplicationBuilder app,
            Action<BusinessHoursOptions>? configure = null)
        {
            var opts = new BusinessHoursOptions();
            configure?.Invoke(opts);
            return app.UseMiddleware<BusinessHoursMiddleware>(opts);
        }
    }
}
