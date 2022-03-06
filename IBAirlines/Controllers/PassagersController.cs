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
    public class PassagersController : Controller
    {
        private readonly DataContext _context;

        public PassagersController(DataContext context)
        {
            _context = context;
        }

        // GET: Passagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Passagers.ToListAsync());
        }

        // GET: Passagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passager = await _context.Passagers
                .FirstOrDefaultAsync(m => m.PassagerID == id);
            if (passager == null)
            {
                return NotFound();
            }

            return View(passager);
        }

        // GET: Passagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassagerID,Nom,PreNom,Ville")] Passager passager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passager);
        }

        // GET: Passagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passager = await _context.Passagers.FindAsync(id);
            if (passager == null)
            {
                return NotFound();
            }
            return View(passager);
        }

        // POST: Passagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassagerID,Nom,PreNom,Ville")] Passager passager)
        {
            if (id != passager.PassagerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassagerExists(passager.PassagerID))
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
            return View(passager);
        }

        // GET: Passagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passager = await _context.Passagers
                .FirstOrDefaultAsync(m => m.PassagerID == id);
            if (passager == null)
            {
                return NotFound();
            }

            return View(passager);
        }

        // POST: Passagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passager = await _context.Passagers.FindAsync(id);
            _context.Passagers.Remove(passager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassagerExists(int id)
        {
            return _context.Passagers.Any(e => e.PassagerID == id);
        }
    }
}
