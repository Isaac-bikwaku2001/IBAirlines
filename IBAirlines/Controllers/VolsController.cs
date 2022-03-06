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
    public class VolsController : Controller
    {
        private readonly DataContext _context;

        public VolsController(DataContext context)
        {
            _context = context;
        }

        // GET: Vols
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Vols.Include(v => v.Avion).Include(v => v.Pilote);
            return View(await dataContext.ToListAsync());
        }

        // GET: Vols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vols
                .Include(v => v.Avion)
                .Include(v => v.Pilote)
                .FirstOrDefaultAsync(m => m.VolID == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
        }

        // GET: Vols/Create
        public IActionResult Create()
        {
            ViewData["AvionID"] = new SelectList(_context.Avions, "AvionID", "Marque");
            ViewData["PiloteID"] = new SelectList(_context.Pilotes, "PiloteID", "Adresse");
            return View();
        }

        // POST: Vols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VolID,AvionID,PiloteID,VilleDepart,VilleArrivee,HeureDepart,HeureArrivee")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvionID"] = new SelectList(_context.Avions, "AvionID", "Marque", vol.AvionID);
            ViewData["PiloteID"] = new SelectList(_context.Pilotes, "PiloteID", "Adresse", vol.PiloteID);
            return View(vol);
        }

        // GET: Vols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vols.FindAsync(id);
            if (vol == null)
            {
                return NotFound();
            }
            ViewData["AvionID"] = new SelectList(_context.Avions, "AvionID", "Marque", vol.AvionID);
            ViewData["PiloteID"] = new SelectList(_context.Pilotes, "PiloteID", "Adresse", vol.PiloteID);
            return View(vol);
        }

        // POST: Vols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VolID,AvionID,PiloteID,VilleDepart,VilleArrivee,HeureDepart,HeureArrivee")] Vol vol)
        {
            if (id != vol.VolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolExists(vol.VolID))
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
            ViewData["AvionID"] = new SelectList(_context.Avions, "AvionID", "Marque", vol.AvionID);
            ViewData["PiloteID"] = new SelectList(_context.Pilotes, "PiloteID", "Adresse", vol.PiloteID);
            return View(vol);
        }

        // GET: Vols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vols
                .Include(v => v.Avion)
                .Include(v => v.Pilote)
                .FirstOrDefaultAsync(m => m.VolID == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
        }

        // POST: Vols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vol = await _context.Vols.FindAsync(id);
            _context.Vols.Remove(vol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolExists(int id)
        {
            return _context.Vols.Any(e => e.VolID == id);
        }
    }
}
