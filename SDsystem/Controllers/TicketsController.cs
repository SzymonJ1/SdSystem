using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDsystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SDsystem.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(string sortOrder, string statusFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["StatusFilter"] = statusFilter;

            var tickets = _context.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                tickets = tickets.Where(t => t.Status == statusFilter);
            }

            tickets = sortOrder switch
            {
                "date_asc" => tickets.OrderBy(t => t.Date),
                "date_desc" => tickets.OrderByDescending(t => t.Date),
                _ => tickets.OrderByDescending(t => t.Date)
            };

            return View(await tickets.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
            return ticket == null ? NotFound() : View(ticket);
        }

        // GET: Tickets/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Description,Name,Surname,Department,Status,Date")] TicketEntity ticket)
        {
            if (!ModelState.IsValid) return View(ticket);

            _context.Add(ticket);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Pomyślnie utworzono zgłoszenie o ID: {ticket.Id}";
            return RedirectToAction("Index", "Home");
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            ViewBag.Statuses = new SelectList(new[] { "Aktywne", "Obsługiwane", "Przydzielone", "Zakończone" }, ticket.Status);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Description,Name,Surname,Department,Status,Date")] TicketEntity ticket)
        {
            if (id != ticket.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Statuses = new SelectList(new[] { "Aktywne", "Obsługiwane", "Przydzielone", "Zakończone" }, ticket.Status);
                return View(ticket);
            }

            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(ticket.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
            return ticket == null ? NotFound() : View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null) _context.Tickets.Remove(ticket);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id) => _context.Tickets.Any(e => e.Id == id);
    }
}