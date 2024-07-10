using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDsystem.Entities;
using System.Threading.Tasks;

public class SearchController : Controller
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Search()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(int id)
    {
        var entity = await _context.Tickets.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        return View("Details", entity);
    }
}
