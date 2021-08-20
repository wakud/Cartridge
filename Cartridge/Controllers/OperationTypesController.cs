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
    public class OperationTypesController : Controller
    {
        private readonly MainContext _context;

        public OperationTypesController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var mainContext = _context.OperationTypes
                .Include(o => o.GetPunkt)
                .Include(o => o.GetStan);

            List<Punkt> punkts = _context.Punkts.ToList();
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name");
            return View(await mainContext.ToListAsync());
        }

        [HttpGet]
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

        [HttpGet]
        public IActionResult Create()
        {
            //List<Punkt> punkts = _context.Punkts.ToList();
            //punkts.Insert(0, new Punkt { Name = "---------", Id = 1 });
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name");
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Name");
            return View();
        }

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

        [HttpGet]
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

        [HttpGet]
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
