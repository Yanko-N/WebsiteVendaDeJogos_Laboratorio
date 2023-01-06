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
    public class JogoCategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JogoCategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> HomeScreen()
        {
           
            var applicationDbContext = _context.JogoModel.Include(j=>j.categoria);
            
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: JogoCategorias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JogoCategoria.Include(j => j.categoria).Include(n => n.jogo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JogoCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JogoCategoria == null)
            {
                return NotFound();
            }

            var jogoCategoria = await _context.JogoCategoria
                .Include(j => j.categoria)
                .Include(j => j.jogo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogoCategoria == null)
            {
                return NotFound();
            }

            return View(jogoCategoria);
        }

        // GET: JogoCategorias/Create
        public IActionResult Create()
        {
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome");
            ViewData["jogoId"] = new SelectList(_context.JogoModel, "Id", "Nome");
            return View();
        }

        // POST: JogoCategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,jogoId,categoriaId")] JogoCategoria jogoCategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogoCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoCategoria.categoriaId);
            ViewData["jogoId"] = new SelectList(_context.JogoModel, "Id", "Nome", jogoCategoria.jogoId);
            return View(jogoCategoria);
        }

        // GET: JogoCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JogoCategoria == null)
            {
                return NotFound();
            }

            var jogoCategoria = await _context.JogoCategoria.FindAsync(id);
            if (jogoCategoria == null)
            {
                return NotFound();
            }
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoCategoria.categoriaId);
            ViewData["jogoId"] = new SelectList(_context.JogoModel, "Id", "Nome", jogoCategoria.jogoId);
            return View(jogoCategoria);
        }

        // POST: JogoCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,jogoId,categoriaId")] JogoCategoria jogoCategoria)
        {
            if (id != jogoCategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogoCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogoCategoriaExists(jogoCategoria.Id))
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
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoCategoria.categoriaId);
            ViewData["jogoId"] = new SelectList(_context.JogoModel, "Id", "Nome", jogoCategoria.jogoId);
            return View(jogoCategoria);
        }

        // GET: JogoCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JogoCategoria == null)
            {
                return NotFound();
            }

            var jogoCategoria = await _context.JogoCategoria
                .Include(j => j.categoria)
                .Include(j => j.jogo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogoCategoria == null)
            {
                return NotFound();
            }

            return View(jogoCategoria);
        }

        // POST: JogoCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JogoCategoria == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JogoCategoria'  is null.");
            }
            var jogoCategoria = await _context.JogoCategoria.FindAsync(id);
            if (jogoCategoria != null)
            {
                _context.JogoCategoria.Remove(jogoCategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogoCategoriaExists(int id)
        {
          return _context.JogoCategoria.Any(e => e.Id == id);
        }
    }
}
