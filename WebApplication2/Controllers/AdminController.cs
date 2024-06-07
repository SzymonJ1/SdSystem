using Microsoft.AspNetCore.Mvc;
using SdSystem.Data;
using SdSystem.Models;
using System.Linq;

namespace SdSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly SdSystemContext _context;

        public AdminController(SdSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var coordinators = _context.Users.Where(u => u.Role == Role.Coordinator).ToList();
            return View(coordinators);
        }

        public IActionResult CreateCoordinator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCoordinator(User model)
        {
            if (ModelState.IsValid)
            {
                model.Role = Role.Coordinator;
                _context.Users.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
