using Cartridge.Data;
using Cartridge.Models;
using Cartridge.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cartridge.Controllers
{
    public class OperationController : Controller
    {
        private readonly MainContext db;
        public OperationController(MainContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Reception(int id, int? location, int filtr)
        {
            OperationType ot = db.OperationTypes
                .Include(ot => ot.GetPunkt)
                .Include(ot => ot.GetStan)
                .FirstOrDefault(ot => ot.Id == id);

            if (ot == null)
            {
                return NotFound();
            }

            IEnumerable<Cartridges> cart = db.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .Where(c => c.DateDel == null);

            List<Punkt> company = db.Punkts
                .Select(p => new Punkt { Id = p.Id, Name = p.Name })
                .ToList();
            company.Insert(0, new Punkt { Id = 0, Name = "--- Всі ---" });

            IndexViewModel ivm = new IndexViewModel { operations = ot, cartridges = cart, punkts = company, selectedLocationId = location };

            if (location != null && location > 0)
            {
                Console.WriteLine("------------------");
                Console.WriteLine(filtr);
                ivm.cartridges = cart.Where(p => p.PunktId == location);
            }


            ViewData["Punkts"] = new SelectList(db.Punkts, "Id", "Name");
            ViewData["Stans"] = new SelectList(db.Stans, "Id", "Name");

            return View(ivm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reception(string? code, int id, int? new_location, int? New_local,
                                                    string? is_filled, int? location)
        {
            if (code == null)
            {
                return NotFound();
            }
            
            OperationType ot = db.OperationTypes
                .Include(ot => ot.GetPunkt)
                .Include(ot => ot.GetStan)
                .FirstOrDefault(ot => ot.Id == id);

            var cartridge = await db.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .FirstOrDefaultAsync(c => c.Code == int.Parse(code) && c.DateDel == null);

            if (cartridge == null)
            {
                return NotFound();
            }

            if (new_location == null)
            {
                new_location = New_local;
            }
           
            
            Punkt newLoc = ot.GetPunkt != null ? ot.GetPunkt : db.Punkts.FirstOrDefault(p => p.Id == new_location);
            if (ModelState.IsValid)
            {
                Operations operation = new Operations
                {
                    Cartridge = cartridge,
                    DateOperation = DateTime.Now,
                    NextPunkt = newLoc,
                    PrevPunkt = cartridge.GetPunkt,
                    Type = ot
                };
                cartridge.GetStan = ot.GetStan;
                cartridge.GetPunkt = newLoc;
                cartridge.Status = is_filled != null;
                db.Operation.Add(operation);
                db.SaveChanges();
            }

            List<Punkt> company = db.Punkts
                .Select(p => new Punkt { Id = p.Id, Name = p.Name })
                .ToList();
            company.Insert(0, new Punkt { Id = 0, Name = "--- Всі ---" });

            IEnumerable<Cartridges> cart = db.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .Where(c => c.DateDel == null && c.PunktId == location);

            IndexViewModel ivm = new IndexViewModel { operations = ot, cartridges = cart, punkts = company, selectedLocationId = location };

            ViewData["Punkts"] = new SelectList(db.Punkts, "Id", "Name");
            ViewData["Added"] = true;

            return View(ivm);
        }
    }
}
