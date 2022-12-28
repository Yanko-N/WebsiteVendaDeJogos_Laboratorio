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
    public class PermissoesModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermissoesModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PermissoesModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.PermissoesModel.ToListAsync());
        }

        // GET: PermissoesModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PermissoesModel == null)
            {
                return NotFound();
            }

            var permissoesModel = await _context.PermissoesModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permissoesModel == null)
            {
                return NotFound();
            }

            return View(permissoesModel);
        }

        // GET: PermissoesModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PermissoesModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] PermissoesModel permissoesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permissoesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permissoesModel);
        }

        // GET: PermissoesModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PermissoesModel == null)
            {
                return NotFound();
            }

            var permissoesModel = await _context.PermissoesModel.FindAsync(id);
            if (permissoesModel == null)
            {
                return NotFound();
            }
            return View(permissoesModel);
        }

        // POST: PermissoesModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] PermissoesModel permissoesModel)
        {
            if (id != permissoesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permissoesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissoesModelExists(permissoesModel.Id))
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
            return View(permissoesModel);
        }

        // GET: PermissoesModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PermissoesModel == null)
            {
                return NotFound();
            }

            var permissoesModel = await _context.PermissoesModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permissoesModel == null)
            {
                return NotFound();
            }

            return View(permissoesModel);
        }

        // POST: PermissoesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PermissoesModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PermissoesModel'  is null.");
            }
            var permissoesModel = await _context.PermissoesModel.FindAsync(id);
            if (permissoesModel != null)
            {
                _context.PermissoesModel.Remove(permissoesModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissoesModelExists(int id)
        {
          return _context.PermissoesModel.Any(e => e.Id == id);
        }
    }
}
