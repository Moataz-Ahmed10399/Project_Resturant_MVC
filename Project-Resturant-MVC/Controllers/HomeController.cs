using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Resturant_MVC.Models;
using System.Diagnostics;

namespace Project_Resturant_MVC.Controllers
{
    [Route("[controller]")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("~/")]

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("privacy")]

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("error")]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet("~/closed")]
        [AllowAnonymous]
        public IActionResult Closed([FromQuery] int start = 9, [FromQuery] int end = 23, [FromQuery] string? returnUrl = null)
        {
            ViewData["Title"] = "Closed Now";
            // صفحة بسيطة تستخدم الـ Partial
            return View(model: (start, end, returnUrl));
        }



    }
}
