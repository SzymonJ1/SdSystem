using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDsystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace SDsystem.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") == "Coordinator")
            {
                // Określamy, jak ma wyglądać domyślne sortowanie
                ViewData["DateSortParam"] = String.IsNullOrEmpty("sortOrder") ? "date_desc" : "";

                var tickets = from t in _context.Tickets
                              select t;

                // Sortowanie po dacie
                switch ("sortOrder")
                {
                    case "date_desc":
                        tickets = tickets.OrderByDescending(t => t.Date); // Sortowanie malejąco
                        break;
                    case "date_asc":
                        tickets = tickets.OrderBy(t => t.Date); // Sortowanie rosnąco
                        break;
                    default:
                        tickets = tickets.OrderByDescending(t => t.Date); // Domyślne sortowanie malejąco
                        break;
                }

                return View(await tickets.ToListAsync());

            }


            return RedirectToAction("Login", "Account");
        }
        

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Coordinator")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ticketEntity = await _context.Tickets
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (ticketEntity == null)
                {
                    return NotFound();
                }

                return View(ticketEntity);
            }
            return RedirectToAction("Login", "Account");
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
        public async Task<IActionResult> Create([Bind("Id,Subject,Description,Name,Surname,Department,Status,Date")] TicketEntity ticketEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketEntity);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Pomyślnie utworzono zgłoszenie o ID: {ticketEntity.Id}. Skopiuj ID, aby sprawdzać status zgłoszenia.";
                return RedirectToAction("Index", "Home");
            }
            return View(ticketEntity);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Coordinator")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ticketEntity = await _context.Tickets.FindAsync(id);
                if (ticketEntity == null)
                {
                    return NotFound();
                }
                // Lista statusów
                ViewBag.Statuses = new SelectList(new List<string> { "Aktywne", "Obsługiwane", "Przydzielone", "Zakończone" }, ticketEntity.Status);
                return View(ticketEntity);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Description,Name,Surname,Department,Status,Date")] TicketEntity ticketEntity)
        {
            if (HttpContext.Session.GetString("Role") == "Coordinator")
            {
                if (id != ticketEntity.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(ticketEntity);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TicketEntityExists(ticketEntity.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Statuses = new SelectList(new List<string> { "Aktywne", "Obsługiwane", "Przydzielone", "Zakończone" }, ticketEntity.Status);
                return View(ticketEntity);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Coordinator")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ticketEntity = await _context.Tickets
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (ticketEntity == null)
                {
                    return NotFound();
                }

                return View(ticketEntity);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Role") == "Coordinator")
            {
                var ticketEntity = await _context.Tickets.FindAsync(id);
                if (ticketEntity != null)
                {
                    _context.Tickets.Remove(ticketEntity);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "Account");
        }

        private bool TicketEntityExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
