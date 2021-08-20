using Cartridge.Data;
using Cartridge.Models;
using Cartridge.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cartridge.Controllers
{
    [Authorize]
    public class ZvityController : Controller
    {
        private readonly MainContext db;
        public ZvityController(MainContext context)
        {
            db = context;
        }

        // GET: Zvity
        public ActionResult Zvity()
        {
            return View();
        }

        public ActionResult Sklad_Empty()
        {
            IEnumerable<Cartridges> cart = db.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .Where(c => c.GetPunkt.Id == 1 && c.Status == false);

            IndexViewModel ivm = new IndexViewModel { cartridges = cart };

            ViewData["Punkts"] = new SelectList(db.Punkts, "Id", "Name");
            ViewData["Stans"] = new SelectList(db.Stans, "Id", "Name");

            return View(ivm);
        }

        public ActionResult Service()
        {
            IEnumerable<Cartridges> cart = db.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .Where(c => c.GetPunkt.Id == 2);

            IndexViewModel ivm = new IndexViewModel { cartridges = cart };

            ViewData["Punkts"] = new SelectList(db.Punkts, "Id", "Name");
            ViewData["Stans"] = new SelectList(db.Stans, "Id", "Name");

            return View(ivm);
        }

        public ActionResult Sklad_Refueled()
        {
            IEnumerable<Cartridges> cart = db.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .Where(c => c.GetPunkt.Id == 1 && c.Status == true);

            IndexViewModel ivm = new IndexViewModel { cartridges = cart };

            ViewData["Punkts"] = new SelectList(db.Punkts, "Id", "Name");
            ViewData["Stans"] = new SelectList(db.Stans, "Id", "Name");

            return View(ivm);
        }

        public ActionResult InWork()
        {
            IEnumerable<Cartridges> cart = db.Cartridges
                .Include(c => c.GetModelCartridge)
                .ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .Where(c => c.GetPunkt.Id != 1 && c.GetPunkt.Id != 2);

            IndexViewModel ivm = new IndexViewModel { cartridges = cart };

            ViewData["Punkts"] = new SelectList(db.Punkts, "Id", "Name");
            ViewData["Stans"] = new SelectList(db.Stans, "Id", "Name");

            return View(ivm);
        }

    }
}
