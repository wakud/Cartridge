using Cartridge.Data;
using Cartridge.Models;
using Cartridge.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using SharpDocx;

namespace Cartridge.Controllers
{
    [Authorize]
    public class OperationController : Controller
    {
        private readonly MainContext db;
        private readonly IWebHostEnvironment appEnvir;
        public OperationController(MainContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            appEnvir = appEnvironment;
            Console.OutputEncoding = Encoding.GetEncoding(1251);
        }

        [HttpGet]
        public IActionResult Reception(int id, int? location, int new_location)
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
                .Include(c => c.GetModelCartridge).ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .Where(c => c.DateDel == null);

            List<Punkt> company = db.Punkts
                .Select(p => new Punkt { Id = p.Id, Name = p.Name })
                .ToList();
            company.Insert(0, new Punkt { Id = 0, Name = "--- Ввиберіть ---" });

            IndexViewModel ivm = new IndexViewModel 
            { 
                operationsType = ot, 
                cartridges = cart, 
                punkts = company, 
                newPunkt = new_location,
                selectedLocationId = location 
            };

            if (location != null && location > 0)
            {
                ivm.cartridges = cart.Where(p => p.PunktId == location);
            }

            ViewData["Stans"] = new SelectList(db.Stans, "Id", "Name");
            
            return View(ivm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reception(string? code, int id, int? new_location, string? is_filled,
            int? location, string aktik, string knopka)
        {

            //Формування акту
            if (aktik != null)
            {
                string filePath = "\\Files\\";
                string generatedPath = "Akt.docx";
                string fileName = "shablon_Akt.docx";
                string fullPath = appEnvir.WebRootPath + filePath + fileName;
                string fullGenerated = appEnvir.WebRootPath + filePath + generatedPath;

                if (location != 0 || location != null && new_location != 0 || new_location != null && id != 0)
                {
                    //робимо список картриджів для акту
                    IEnumerable<Operations> Act = db.Operation
                        .Include(o => o.Cartridge).ThenInclude(c => c.GetModelCartridge)
                        .Where(o => o.Type.Id == id
                            && o.DateOperation.Date == DateTime.Now.Date
                            && o.PrevPunktId == location && o.NextPunktId == new_location);

                    Punkt prewPunkt = db.Punkts.FirstOrDefault(p => p.Id == location);
                    Punkt newPunkt = db.Punkts.FirstOrDefault(p => p.Id == new_location);

                    AktModel aktModel = new AktModel()
                    {
                        Operations = Act,
                        prevPunkt = prewPunkt.Name,
                        newPunkt = newPunkt.Name
                    };

                    DocumentBase document = DocumentFactory.Create(fullPath, aktModel);
                    document.Generate(fullGenerated);

                    string NewFileName = "Akt_" + DateTime.Now.ToString("d") + ".docx";
                    return File(
                        System.IO.File.ReadAllBytes(fullGenerated),
                        System.Net.Mime.MediaTypeNames.Application.Octet,
                        NewFileName
                    );
                }
            }

            //Для операцій над картриджами
            if (code == null)
            {
                return NotFound();
            }

            OperationType ot = db.OperationTypes
                .Include(ot => ot.GetPunkt)
                .Include(ot => ot.GetStan)
                .FirstOrDefault(ot => ot.Id == id);

            var cartridge = await db.Cartridges
                .Include(c => c.GetModelCartridge).ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .FirstOrDefaultAsync(c => c.Code == int.Parse(code) && c.DateDel == null);
            
            if (cartridge == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("new_location - " + new_location);
                Punkt newLoc = ot.GetPunkt != null ? ot.GetPunkt : db.Punkts.FirstOrDefault(p => p.Id == new_location);
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

            List<Punkt> company = db.Punkts.Select(p => new Punkt { Id = p.Id, Name = p.Name }).ToList();
            company.Insert(0, new Punkt { Id = 0, Name = "--- Ввиберіть ---" });

            IEnumerable<Cartridges> cart = db.Cartridges
                .Include(c => c.GetModelCartridge).ThenInclude(c => c.Printers)
                .Include(c => c.GetPunkt)
                .Include(c => c.GetStan)
                .Where(c => c.DateDel == null && c.PunktId == location);
            IndexViewModel ivm = new IndexViewModel() { operationsType = ot, cartridges = cart, 
                punkts = company, selectedLocationId = location, newPunkt = new_location };

            ViewData["Added"] = true;
            
            return View(ivm);
        }
    }
}
