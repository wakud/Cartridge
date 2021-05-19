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
    public class OperationTypesController : Controller
    {
        private readonly MainContext _context;

        public OperationTypesController(MainContext context)
        {
            _context = context;
        }

        // GET: OperationTypes
        public async Task<IActionResult> Index()
        {
            var mainContext = _context.OperationTypes.Include(o => o.GetPunkt).Include(o => o.GetStan);
            return View(await mainContext.ToListAsync());
        }

        // GET: OperationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationType = await _context.OperationTypes
                .Include(o => o.GetPunkt)
                .Include(o => o.GetStan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operationType == null)
            {
                return NotFound();
            }

            return View(operationType);
        }

        // GET: OperationTypes/Create
        public IActionResult Create()
        {
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name");
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Name");
            return View();
        }

        // POST: OperationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FillDefCheck,PunktId,StanId")] OperationType operationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Id", operationType.PunktId);
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Id", operationType.StanId);
            return View(operationType);
        }

        // GET: OperationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationType = await _context.OperationTypes.FindAsync(id);
            if (operationType == null)
            {
                return NotFound();
            }
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name", operationType.PunktId);
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Name", operationType.StanId);
            return View(operationType);
        }

        // POST: OperationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FillDefCheck,PunktId,StanId")] OperationType operationType)
        {
            if (id != operationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationTypeExists(operationType.Id))
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
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Id", operationType.PunktId);
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Id", operationType.StanId);
            return View(operationType);
        }

        // GET: OperationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationType = await _context.OperationTypes
                .Include(o => o.GetPunkt)
                .Include(o => o.GetStan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operationType == null)
            {
                return NotFound();
            }

            return View(operationType);
        }

        // POST: OperationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operationType = await _context.OperationTypes.FindAsync(id);
            _context.OperationTypes.Remove(operationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationTypeExists(int id)
        {
            return _context.OperationTypes.Any(e => e.Id == id);
        }
    }
}
