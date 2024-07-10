﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SDsystem.Entities;

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
            return View(await _context.Tickets.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Description,Name,Surname,Department,Status,Date")] TicketEntity ticketEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketEntity);
                await _context.SaveChangesAsync();

                // Przechowaj wiadomość w TempData
                TempData["SuccessMessage"] = $"Pomyślnie utworzono zgłoszenie o ID: {ticketEntity.Id}. Skopiuj ID, aby sprawdzać status zgłoszenia.";
                return RedirectToAction("Index", "Home");
            }
            return View(ticketEntity);
        }


        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(ticketEntity);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Description,Name,Surname,Department,Status,Date")] TicketEntity ticketEntity)
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
            return View(ticketEntity);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketEntity = await _context.Tickets.FindAsync(id);
            if (ticketEntity != null)
            {
                _context.Tickets.Remove(ticketEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketEntityExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
