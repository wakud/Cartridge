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
    public class CartridgesController : Controller
    {
        private readonly MainContext _context;

        public CartridgesController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IQueryable<Cartridges> mainContext = _context.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Where(c => c.DateDel == null);

            return View(await mainContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartridges = await _context.Cartridges
                .Include(c => c.GetModelCartridge).ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.Operation).ThenInclude(o => o.Type)
                .Include(c => c.Operation).ThenInclude(o => o.PrevPunkt)
                .Include(c => c.Operation).ThenInclude(o => o.NextPunkt)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cartridges == null)
            {
                return NotFound();
            }
            
            return View(cartridges);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModels, "Id", "Name");
            ViewData["ModelPrinterId"] = new SelectList(_context.PrintersModels, "Id", "Name");
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name");
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Code, [Bind("Id,Code,DateInsert,DateDel,PunktId,ModelCartridgeId,ModelPrinterId,Status,StanId")] Cartridges cartridges)
        {
            if (ModelState.IsValid)
            {
                List<Cartridges> cart = _context.Cartridges.ToList();
                foreach(var item in cart)
                {
                    if (Code == item.Code)
                    {
                        ViewBag.error = "BadCode";
                        ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModels, "Id", "Name", cartridges.ModelCartridgeId);
                        ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name", cartridges.PunktId);
                        ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Name", cartridges.StanId);
                        return View(cartridges);
                    }
                }

                _context.Add(cartridges);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModels, "Id", "Id", cartridges.ModelCartridgeId);
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Id", cartridges.PunktId);
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Name", cartridges.StanId);
            
            return View(cartridges);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartridges = await _context.Cartridges.FindAsync(id);
            if (cartridges == null)
            {
                return NotFound();
            }
            ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModels, "Id", "Name", cartridges.ModelCartridgeId);
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name", cartridges.PunktId);
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Name", cartridges.StanId);

            return View(cartridges);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,DateInsert,DateDel,PunktId,ModelCartridgeId,ModelPrinterId,Status,StanId")] Cartridges cartridges)
        {
            if (id != cartridges.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartridges);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartridgesExists(cartridges.Id))
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
            ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModels, "Id", "Id", cartridges.ModelCartridgeId);
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Id", cartridges.PunktId);
            ViewData["StanId"] = new SelectList(_context.Stans, "Id", "Id", cartridges.StanId);

            return View(cartridges);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartridges = await _context.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartridges == null)
            {
                return NotFound();
            }

            return View(cartridges);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartridges = await _context.Cartridges.FindAsync(id);
            _context.Cartridges.Remove(cartridges);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartridgesExists(int id)
        {
            return _context.Cartridges.Any(e => e.Id == id);
        }
    }
}
