using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cartridge.Data;
using Cartridge.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cartridge.Controllers
{
    [Authorize]
    public class PunktsController : Controller
    {
        private readonly MainContext _context;

        public PunktsController(MainContext context)
        {
            _context = context;
        }

        // GET: Punkts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Punkts.ToListAsync());
        }

        // GET: Punkts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punkt = await _context.Punkts
                .Include(m => m.Printers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (punkt == null)
            {
                return NotFound();
            }

            return View(punkt);
        }

        // GET: Punkts/Create
        public IActionResult Create()
        {
            return View(_context.PrintersModels.ToList());
        }

        // POST: Punkts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Punkt punkt, int[] selectedPrint)
        {
            if (ModelState.IsValid)
            {
                _context.Punkts.Add(punkt);
                await _context.SaveChangesAsync();
                if (selectedPrint != null)
                {
                    foreach (var c in _context.PrintersModels.Where(pr => selectedPrint.Contains(pr.Id)))
                        punkt.Printers.Add(c);        
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(punkt);
        }

        // GET: Punkts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Punkt punkt = await _context.Punkts.Include(p => p.Printers).FirstOrDefaultAsync(p => p.Id == id);
            
            if (punkt == null)
            {
                return NotFound();
            }
            ViewBag.ModelPrinter = _context.PrintersModels.ToList();
            return View(punkt);
        }

        // POST: Punkts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int[] selectedPrint, Punkt punkt, int id)
        {
            if (id != punkt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Punkt NewPunkt = await _context.Punkts.Include(p => p.Printers).FirstOrDefaultAsync(p => p.Id == id);
                    NewPunkt.Name = punkt.Name;
                    if (selectedPrint != null)
                    {
                        //отримуємо вибрані принтери
                        foreach (var c in _context.PrintersModels.Where(pr => selectedPrint.Contains(pr.Id)))
                        {
                            if (!NewPunkt.Printers.Contains(c))
                            {
                                NewPunkt.Printers.Add(c);
                            }
                        }
                    }

                    _context.Update(NewPunkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!PunktExists(punkt.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                        throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(punkt);
        }

        // GET: Punkts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punkt = await _context.Punkts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (punkt == null)
            {
                return NotFound();
            }

            return View(punkt);
        }

        // POST: Punkts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var punkt = await _context.Punkts.FindAsync(id);
            _context.Punkts.Remove(punkt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PunktExists(int id)
        {
            return _context.Punkts.Any(e => e.Id == id);
        }
    }
}
