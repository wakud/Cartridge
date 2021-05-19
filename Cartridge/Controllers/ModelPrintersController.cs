using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cartridge.Data;
using Cartridge.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Cartridge.Controllers
{
    public class ModelPrintersController : Controller
    {
        private readonly MainContext _context;
        private readonly IWebHostEnvironment appEnv;
        public static IConfiguration Configuration;

        public ModelPrintersController(MainContext context, IWebHostEnvironment appEnviroment)
        {
            _context = context;
            appEnv = appEnviroment;
        }

        // GET: ModelPrinters
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrintersModels.ToListAsync());
        }

        // GET: ModelPrinters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelPrinter = await _context.PrintersModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelPrinter == null)
            {
                return NotFound();
            }

            return View(modelPrinter);
        }

        // GET: ModelPrinters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelPrinters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Name, IFormFile files)
        {
            string filePath = "\\Images\\Printer\\";
            string fullPath = appEnv.WebRootPath + filePath;

            if (Name != null)
            {
                if (files != null)
                {
                    fullPath += files.FileName;
                    //зберігаємо файл
                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    files.CopyTo(fileStream);
                }
                else
                {
                    fullPath = appEnv.WebRootPath + "\\Images\\images.png";
                }

                if (ModelState.IsValid)
                {
                    ModelPrinter printer = new()
                    {
                        Name = Name,
                        Photo = fullPath
                    };
                    _context.Add(printer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: ModelPrinters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ModelPrinter modelPrinter = await _context.PrintersModels.FindAsync(id);
            if (modelPrinter == null)
            {
                return NotFound();
            }
            return View(modelPrinter);
        }

        // POST: ModelPrinters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile files, string Name)
        {
            ModelPrinter modelPrinter = await _context.PrintersModels.FindAsync(id);
            if (id != modelPrinter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string filePath = "\\Images\\Printer\\";

                    if (files != null)
                    {
                        string fullPath = appEnv.WebRootPath + filePath + files.FileName;
                        //зберігаємо файл
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                            files.CopyTo(fileStream);

                        modelPrinter.Name = Name;
                        modelPrinter.Photo = fullPath;
                    }
                    else if (files == null)
                    {
                        modelPrinter.Name = Name;
                    }

                    _context.Update(modelPrinter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelPrinterExists(modelPrinter.Id))
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
            return View(modelPrinter);
        }

        // GET: ModelPrinters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelPrinter = await _context.PrintersModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelPrinter == null)
            {
                return NotFound();
            }

            return View(modelPrinter);
        }

        // POST: ModelPrinters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelPrinter = await _context.PrintersModels.FindAsync(id);
            _context.PrintersModels.Remove(modelPrinter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelPrinterExists(int id)
        {
            return _context.PrintersModels.Any(e => e.Id == id);
        }

        public async Task<IActionResult> PrinterImage(int Id)
        {
            ModelPrinter printer = _context.PrintersModels.FirstOrDefault(p => p.Id == Id);
            if (printer == null)
            {
                return Content("Принтер не знайдено");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(printer.Photo, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, Utils.ImagePath.GetContentType(printer.Photo), Path.GetFileName(printer.Photo));
        }
    }
}
