using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SDsystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Akcja do wylogowania
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Wyloguj użytkownika
            return RedirectToAction("Login", "Account"); // Przekieruj na stronę logowania
        }
    }
}