using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using SDsystem.Entities;

namespace SDsystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == model.UserName && u.PasswordHash == model.PasswordHash);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.UserName);
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
            return RedirectToAction("Index", "Home");
        }
    }
}
