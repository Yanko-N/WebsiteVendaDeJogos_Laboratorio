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
    public class CargoPermissoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CargoPermissoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CargoPermissoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.CargoPermissoes.ToListAsync());
        }

        // GET: CargoPermissoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CargoPermissoes == null)
            {
                return NotFound();
            }

            var cargoPermissoes = await _context.CargoPermissoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargoPermissoes == null)
            {
                return NotFound();
            }

            return View(cargoPermissoes);
        }

        // GET: CargoPermissoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CargoPermissoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] CargoPermissoes cargoPermissoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargoPermissoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cargoPermissoes);
        }

        // GET: CargoPermissoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CargoPermissoes == null)
            {
                return NotFound();
            }

            var cargoPermissoes = await _context.CargoPermissoes.FindAsync(id);
            if (cargoPermissoes == null)
            {
                return NotFound();
            }
            return View(cargoPermissoes);
        }

        // POST: CargoPermissoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] CargoPermissoes cargoPermissoes)
        {
            if (id != cargoPermissoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargoPermissoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoPermissoesExists(cargoPermissoes.Id))
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
            return View(cargoPermissoes);
        }

        // GET: CargoPermissoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CargoPermissoes == null)
            {
                return NotFound();
            }

            var cargoPermissoes = await _context.CargoPermissoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargoPermissoes == null)
            {
                return NotFound();
            }

            return View(cargoPermissoes);
        }

        // POST: CargoPermissoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CargoPermissoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CargoPermissoes'  is null.");
            }
            var cargoPermissoes = await _context.CargoPermissoes.FindAsync(id);
            if (cargoPermissoes != null)
            {
                _context.CargoPermissoes.Remove(cargoPermissoes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoPermissoesExists(int id)
        {
          return _context.CargoPermissoes.Any(e => e.Id == id);
        }
    }
}
