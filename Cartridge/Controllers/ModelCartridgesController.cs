using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cartridge.Data;
using Cartridge.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Cartridge.Controllers
{
    [Authorize]
    public class ModelCartridgesController : Controller
    {
        private readonly MainContext _context;
        private readonly IWebHostEnvironment webHost;
        public static IConfiguration Configuration;

        public ModelCartridgesController(MainContext context, IWebHostEnvironment environment)
        {
            _context = context;
            webHost = environment;
        }

        // GET: ModelCartridges
        public async Task<IActionResult> Index()
        {
            return View(await _context.CartridgesModels.ToListAsync());
        }

        // GET: ModelCartridges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelCartridge = await _context.CartridgesModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelCartridge == null)
            {
                return NotFound();
            }

            return View(modelCartridge);
        }

        // GET: ModelCartridges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelCartridges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string description, bool baraban, IFormFile files)
        {
            string filePath = "\\Images\\Cartridge\\";
            string fullPath = webHost.WebRootPath + filePath;
            if (name != null)
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
                    fullPath = webHost.WebRootPath + "\\Images\\images.png";
                }

                if (ModelState.IsValid)
                {
                    ModelCartridge modelCartridge = new()
                    {
                         Name = name,
                         Description = description,
                         Baraban = baraban,
                         Photo = fullPath
                    };
                    _context.Add(modelCartridge);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: ModelCartridges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelCartridge = await _context.CartridgesModels.FindAsync(id);
            if (modelCartridge == null)
            {
                return NotFound();
            }
            return View(modelCartridge);
        }

        // POST: ModelCartridges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, string description, bool baraban, IFormFile files)
        {
            ModelCartridge modelCartridge = await _context.CartridgesModels.FindAsync(id);
            if (id != modelCartridge.Id)
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
                        string fullPath = webHost.WebRootPath + filePath + files.FileName;
                        //зберігаємо файл
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                            files.CopyTo(fileStream);

                        modelCartridge.Name = name;
                        modelCartridge.Description = description;
                        modelCartridge.Baraban = baraban;
                        modelCartridge.Photo = fullPath;
                    }
                    else if (files == null)
                    {
                        modelCartridge.Name = name;
                        modelCartridge.Description = description;
                        modelCartridge.Baraban = baraban;
                    }
                    _context.Update(modelCartridge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelCartridgeExists(modelCartridge.Id))
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
            return View(modelCartridge);
        }

        // GET: ModelCartridges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelCartridge = await _context.CartridgesModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelCartridge == null)
            {
                return NotFound();
            }

            return View(modelCartridge);
        }

        // POST: ModelCartridges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelCartridge = await _context.CartridgesModels.FindAsync(id);
            _context.CartridgesModels.Remove(modelCartridge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelCartridgeExists(int id)
        {
            return _context.CartridgesModels.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CartridgeImage(int Id)
        {
            ModelCartridge cartridges = _context.CartridgesModels.FirstOrDefault(p => p.Id == Id);
            if (cartridges == null)
            {
                return Content("Картридж не знайдено");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(cartridges.Photo, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, Utils.ImagePath.GetContentType(cartridges.Photo), Path.GetFileName(cartridges.Photo));
        }
    }
}
