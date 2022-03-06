using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IBAirlines.Data;
using IBAirlines.Models;
using Microsoft.AspNetCore.Authorization;

namespace IBAirlines.Controllers
{
    [Authorize]
    public class PilotesController : Controller
    {
        private readonly DataContext _context;

        public PilotesController(DataContext context)
        {
            _context = context;
        }

        // GET: Pilotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pilotes.ToListAsync());
        }

        // GET: Pilotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pilote = await _context.Pilotes
                .FirstOrDefaultAsync(m => m.PiloteID == id);
            if (pilote == null)
            {
                return NotFound();
            }

            return View(pilote);
        }

        // GET: Pilotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pilotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PiloteID,Nom,PreNom,Adresse,CodePostal,Ville,Tel,DateNaissance,Salaire")] Pilote pilote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pilote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pilote);
        }

        // GET: Pilotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pilote = await _context.Pilotes.FindAsync(id);
            if (pilote == null)
            {
                return NotFound();
            }
            return View(pilote);
        }

        // POST: Pilotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PiloteID,Nom,PreNom,Adresse,CodePostal,Ville,Tel,DateNaissance,Salaire")] Pilote pilote)
        {
            if (id != pilote.PiloteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pilote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PiloteExists(pilote.PiloteID))
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
            return View(pilote);
        }

        // GET: Pilotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pilote = await _context.Pilotes
                .FirstOrDefaultAsync(m => m.PiloteID == id);
            if (pilote == null)
            {
                return NotFound();
            }

            return View(pilote);
        }

        // POST: Pilotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pilote = await _context.Pilotes.FindAsync(id);
            _context.Pilotes.Remove(pilote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PiloteExists(int id)
        {
            return _context.Pilotes.Any(e => e.PiloteID == id);
        }
    }
}
