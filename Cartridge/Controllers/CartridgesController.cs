﻿using System;
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
    public class CartridgesController : Controller
    {
        private readonly MainContext _context;

        public CartridgesController(MainContext context)
        {
            _context = context;
        }

        // GET: Cartridges
        public async Task<IActionResult> Index()
        {
            var mainContext = _context.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Where(c => c.DateDel == null);

            return View(await mainContext.ToListAsync());
        }

        // GET: Cartridges/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Cartridges/Create
        public IActionResult Create()
        {
            ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModel, "Id", "Name");
            ViewData["ModelPrinterId"] = new SelectList(_context.PrintersModel, "Id", "Name");
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name");
            return View();
        }

        // POST: Cartridges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,DateInsert,DateDel,PunktId,ModelCartridgeId,ModelPrinterId,Status")] Cartridges cartridges)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartridges);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModel, "Id", "Id", cartridges.ModelCartridgeId);
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Id", cartridges.PunktId);
            return View(cartridges);
        }

        // GET: Cartridges/Edit/5
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
            ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModel, "Id", "Name", cartridges.ModelCartridgeId);
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Name", cartridges.PunktId);
            return View(cartridges);
        }

        // POST: Cartridges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,DateInsert,DateDel,PunktId,ModelCartridgeId,ModelPrinterId,Status")] Cartridges cartridges)
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
            ViewData["ModelCartridgeId"] = new SelectList(_context.CartridgesModel, "Id", "Id", cartridges.ModelCartridgeId);
            ViewData["PunktId"] = new SelectList(_context.Punkts, "Id", "Id", cartridges.PunktId);
            return View(cartridges);
        }

        // GET: Cartridges/Delete/5
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

        // POST: Cartridges/Delete/5
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
