using Microsoft.AspNetCore.Mvc;
using SDsystem.Models;
using Microsoft.AspNetCore.Http;

namespace SDsystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly string adminUsername = "admin";
        private readonly string adminPassword = "admin";

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            if (model.Username == adminUsername && model.Password == adminPassword)
            {
                HttpContext.Session.SetString("Username", model.Username);
                HttpContext.Session.SetString("Role", "Coordinator");
                return RedirectToAction("Index", "Tickets");
            }
            else
            {
                ViewBag.Message = "Nieprawidłowe dane logowania";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
