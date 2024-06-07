using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SdSystem.Models;

namespace SdSystem.Controllers
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

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Logika logowania
            // Po zalogowaniu przekieruj u¿ytkownika do odpowiedniego panelu na podstawie roli

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (User.IsInRole("Coordinator"))
            {
                return RedirectToAction("Index", "Coordinator");
            }
            else if (User.IsInRole("Support"))
            {
                return RedirectToAction("Index", "Support");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }

            // Jeœli logowanie nie powiedzie siê, wróæ do strony logowania z komunikatem o b³êdzie
            return View("Index");
        }
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
