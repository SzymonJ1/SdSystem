using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SdSystem.Data;
using SdSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SdSystem.Controllers
{
    public class SupportController : Controller
    {
        private readonly SdSystemContext _context;

        public SupportController(SdSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets.Where(t => t.Status == TicketStatus.InProgress).Include(t => t.CreatedBy).ToListAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Resolve(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Status = TicketStatus.Resolved;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
