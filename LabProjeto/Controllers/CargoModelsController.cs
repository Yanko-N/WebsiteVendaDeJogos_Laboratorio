using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProjeto.Data;
using LabProjeto.Models;

namespace LabProjeto.Controllers
{
    public class CargoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CargoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CargoModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.CargoModel.ToListAsync());
        }

        // GET: CargoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CargoModel == null)
            {
                return NotFound();
            }

            var cargoModel = await _context.CargoModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargoModel == null)
            {
                return NotFound();
            }

            return View(cargoModel);
        }

        // GET: CargoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CargoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] CargoModel cargoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cargoModel);
        }

        // GET: CargoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CargoModel == null)
            {
                return NotFound();
            }

            var cargoModel = await _context.CargoModel.FindAsync(id);
            if (cargoModel == null)
            {
                return NotFound();
            }
            return View(cargoModel);
        }

        // POST: CargoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] CargoModel cargoModel)
        {
            if (id != cargoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoModelExists(cargoModel.Id))
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
            return View(cargoModel);
        }

        // GET: CargoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CargoModel == null)
            {
                return NotFound();
            }

            var cargoModel = await _context.CargoModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargoModel == null)
            {
                return NotFound();
            }

            return View(cargoModel);
        }

        // POST: CargoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CargoModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CargoModel'  is null.");
            }
            var cargoModel = await _context.CargoModel.FindAsync(id);
            if (cargoModel != null)
            {
                _context.CargoModel.Remove(cargoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoModelExists(int id)
        {
          return _context.CargoModel.Any(e => e.Id == id);
        }
    }
}
