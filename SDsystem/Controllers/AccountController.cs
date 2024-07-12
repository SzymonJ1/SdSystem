using Microsoft.AspNetCore.Mvc;
using SDsystem.Models;

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
                return RedirectToAction("Index", "Tickets");
            }
            else
            {
                ViewBag.Message = "Nieprawidłowe dane logowania";
                return View();
            }
        }
    }
}
