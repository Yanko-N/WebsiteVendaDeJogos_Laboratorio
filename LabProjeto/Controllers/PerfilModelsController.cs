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
    public class PerfilModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfilModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PerfilModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PerfilModel.Include(p => p.utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PerfilModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PerfilModel == null)
            {
                return NotFound();
            }

            var perfilModel = await _context.PerfilModel
                .Include(p => p.utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilModel == null)
            {
                return NotFound();
            }

            return View(perfilModel);
        }

        // GET: PerfilModels/Create
        public IActionResult Create()
        {
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PerfilModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,utilizadorId,saldo")] PerfilModel perfilModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfilModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id", perfilModel.utilizadorId);
            return View(perfilModel);
        }

        // GET: PerfilModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PerfilModel == null)
            {
                return NotFound();
            }

            var perfilModel = await _context.PerfilModel.FindAsync(id);
            if (perfilModel == null)
            {
                return NotFound();
            }
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id", perfilModel.utilizadorId);
            return View(perfilModel);
        }

        // POST: PerfilModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,utilizadorId,saldo")] PerfilModel perfilModel)
        {
            if (id != perfilModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilModelExists(perfilModel.Id))
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
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id", perfilModel.utilizadorId);
            return View(perfilModel);
        }

        // GET: PerfilModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PerfilModel == null)
            {
                return NotFound();
            }

            var perfilModel = await _context.PerfilModel
                .Include(p => p.utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilModel == null)
            {
                return NotFound();
            }

            return View(perfilModel);
        }

        // POST: PerfilModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PerfilModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PerfilModel'  is null.");
            }
            var perfilModel = await _context.PerfilModel.FindAsync(id);
            if (perfilModel != null)
            {
                _context.PerfilModel.Remove(perfilModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilModelExists(int id)
        {
          return _context.PerfilModel.Any(e => e.Id == id);
        }
    }
}
