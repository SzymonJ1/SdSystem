using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SdSystem.Data;
using SdSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SdSystem.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly SdSystemContext _context;

        public CoordinatorController(SdSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets.Include(t => t.CreatedBy).Include(t => t.AssignedTo).ToListAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Assign(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var supportUsers = _context.Users.Where(u => u.Role == Role.Support).ToList();
            ViewBag.SupportUsers = supportUsers;

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int ticketId, int assignedToId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket.AssignedToId = assignedToId;
            ticket.Status = TicketStatus.InProgress;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SetPriority(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> SetPriority(int ticketId, Priority priority)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Priority = priority;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CompletedTickets()
        {
            var tickets = await _context.Tickets.Where(t => t.Status == TicketStatus.Resolved).ToListAsync();
            return View(tickets);
        }
    }
}
