using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cartridge.Data;
using Cartridge.Models;

namespace Cartridge.Controllers
{
    public class StansController : Controller
    {
        private readonly MainContext _context;

        public StansController(MainContext context)
        {
            _context = context;
        }

        // GET: Stans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stans.ToListAsync());
        }

        // GET: Stans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stan = await _context.Stans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stan == null)
            {
                return NotFound();
            }

            return View(stan);
        }

        // GET: Stans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Stan stan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stan);
        }

        // GET: Stans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stan = await _context.Stans.FindAsync(id);
            if (stan == null)
            {
                return NotFound();
            }
            return View(stan);
        }

        // POST: Stans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Stan stan)
        {
            if (id != stan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StanExists(stan.Id))
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
            return View(stan);
        }

        // GET: Stans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stan = await _context.Stans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stan == null)
            {
                return NotFound();
            }

            return View(stan);
        }

        // POST: Stans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stan = await _context.Stans.FindAsync(id);
            _context.Stans.Remove(stan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StanExists(int id)
        {
            return _context.Stans.Any(e => e.Id == id);
        }
    }
}
