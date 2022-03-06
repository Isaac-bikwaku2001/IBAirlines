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
    public class AffecteVolsController : Controller
    {
        private readonly DataContext _context;

        public AffecteVolsController(DataContext context)
        {
            _context = context;
        }

        // GET: AffecteVols
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.AffecteVols.Include(a => a.Passager).Include(a => a.Vol);
            return View(await dataContext.ToListAsync());
        }

        // GET: AffecteVols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affecteVol = await _context.AffecteVols
                .Include(a => a.Passager)
                .Include(a => a.Vol)
                .FirstOrDefaultAsync(m => m.AffecteVolID == id);
            if (affecteVol == null)
            {
                return NotFound();
            }

            return View(affecteVol);
        }

        // GET: AffecteVols/Create
        public IActionResult Create()
        {
            ViewData["PassagerID"] = new SelectList(_context.Passagers, "PassagerID", "Nom");
            ViewData["VolID"] = new SelectList(_context.Vols, "VolID", "VilleArrivee");
            return View();
        }

        // POST: AffecteVols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AffecteVolID,PassagerID,VolID,DateVol,NumPlace,Prix")] AffecteVol affecteVol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(affecteVol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PassagerID"] = new SelectList(_context.Passagers, "PassagerID", "Nom", affecteVol.PassagerID);
            ViewData["VolID"] = new SelectList(_context.Vols, "VolID", "VilleArrivee", affecteVol.VolID);
            return View(affecteVol);
        }

        // GET: AffecteVols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affecteVol = await _context.AffecteVols.FindAsync(id);
            if (affecteVol == null)
            {
                return NotFound();
            }
            ViewData["PassagerID"] = new SelectList(_context.Passagers, "PassagerID", "Nom", affecteVol.PassagerID);
            ViewData["VolID"] = new SelectList(_context.Vols, "VolID", "VilleArrivee", affecteVol.VolID);
            return View(affecteVol);
        }

        // POST: AffecteVols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AffecteVolID,PassagerID,VolID,DateVol,NumPlace,Prix")] AffecteVol affecteVol)
        {
            if (id != affecteVol.AffecteVolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(affecteVol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AffecteVolExists(affecteVol.AffecteVolID))
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
            ViewData["PassagerID"] = new SelectList(_context.Passagers, "PassagerID", "Nom", affecteVol.PassagerID);
            ViewData["VolID"] = new SelectList(_context.Vols, "VolID", "VilleArrivee", affecteVol.VolID);
            return View(affecteVol);
        }

        // GET: AffecteVols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affecteVol = await _context.AffecteVols
                .Include(a => a.Passager)
                .Include(a => a.Vol)
                .FirstOrDefaultAsync(m => m.AffecteVolID == id);
            if (affecteVol == null)
            {
                return NotFound();
            }

            return View(affecteVol);
        }

        // POST: AffecteVols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var affecteVol = await _context.AffecteVols.FindAsync(id);
            _context.AffecteVols.Remove(affecteVol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AffecteVolExists(int id)
        {
            return _context.AffecteVols.Any(e => e.AffecteVolID == id);
        }
    }
}
