using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProjeto.Data;
using LabProjeto.Models;
using Microsoft.AspNetCore.Authorization;

namespace LabProjeto.Controllers
{
    public class JogoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JogoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JogoModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JogoModel.Include(j => j.categoria);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: JogoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JogoModel == null)
            {
                return NotFound();
            }

            var jogoModel = await _context.JogoModel
                .Include(j => j.categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogoModel == null)
            {
                return NotFound();
            }

            return View(jogoModel);
        }

        // GET: JogoModels/Create
        public IActionResult Create()
        {
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome");
            return View();
        }

        // POST: JogoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Admin,Funcionario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,categoriaId")] JogoModel jogoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoModel.categoriaId);
            return View(jogoModel);
        }

        // GET: JogoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JogoModel == null)
            {
                return NotFound();
            }

            var jogoModel = await _context.JogoModel.FindAsync(id);
            if (jogoModel == null)
            {
                return NotFound();
            }
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoModel.categoriaId);
            return View(jogoModel);
        }

        // POST: JogoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,categoriaId")] JogoModel jogoModel)
        {
            if (id != jogoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogoModelExists(jogoModel.Id))
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
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoModel.categoriaId);
            return View(jogoModel);
        }

        // GET: JogoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JogoModel == null)
            {
                return NotFound();
            }

            var jogoModel = await _context.JogoModel
                .Include(j => j.categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogoModel == null)
            {
                return NotFound();
            }

            return View(jogoModel);
        }

        // POST: JogoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JogoModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JogoModel'  is null.");
            }
            var jogoModel = await _context.JogoModel.FindAsync(id);
            if (jogoModel != null)
            {
                _context.JogoModel.Remove(jogoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogoModelExists(int id)
        {
          return _context.JogoModel.Any(e => e.Id == id);
        }
    }
}
