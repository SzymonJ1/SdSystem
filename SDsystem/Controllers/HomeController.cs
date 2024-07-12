using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDsystem.Entities;
using SDsystem.Models;
using System.Diagnostics;

namespace SDsystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            ViewBag.ErrorMessage = "Brak dostêpu do zg³oszeñ, zaloguj siê do panelu koordynatora.";
            return View();
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
