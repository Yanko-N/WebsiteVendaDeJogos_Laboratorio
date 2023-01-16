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
using System.Data;

namespace LabProjeto.Controllers
{
    public class CategoriaModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoriaModels
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.CategoriaModel.ToListAsync());
        }

        // GET: CategoriaModels/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoriaModel == null)
            {
                return NotFound();
            }

            var categoriaModel = await _context.CategoriaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaModel == null)
            {
                return NotFound();
            }

            return View(categoriaModel);
        }

        // GET: CategoriaModels/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] CategoriaModel categoriaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaModel);
        }

        // GET: CategoriaModels/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoriaModel == null)
            {
                return NotFound();
            }

            var categoriaModel = await _context.CategoriaModel.FindAsync(id);
            if (categoriaModel == null)
            {
                return NotFound();
            }
            return View(categoriaModel);
        }

        // POST: CategoriaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] CategoriaModel categoriaModel)
        {
            if (id != categoriaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaModelExists(categoriaModel.Id))
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
            return View(categoriaModel);
        }

        // GET: CategoriaModels/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || _context.CategoriaModel == null)
            {
                return NotFound();
            }

            var categoriaModel = await _context.CategoriaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaModel == null)
            {
                return NotFound();
            }

            return View(categoriaModel);
        }

        // POST: CategoriaModels/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //REMOVER categora DE perfil CATEGORIA
            if (_context.PerfilCategoria.Any(c=>c.categoriaId == id))
            {
                var categoria = _context.PerfilCategoria.Single(o => o.categoriaId == id);
                if (categoria != null)
                {
                    _context.PerfilCategoria.Remove(categoria);
                    await _context.SaveChangesAsync();
                }
            }
            

            if (_context.CategoriaModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CategoriaModel'  is null.");
            }
            var categoriaModel = await _context.CategoriaModel.FindAsync(id);
            if (categoriaModel != null)
            {
                _context.CategoriaModel.Remove(categoriaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaModelExists(int id)
        {
          return _context.CategoriaModel.Any(e => e.Id == id);
        }
    }
}
