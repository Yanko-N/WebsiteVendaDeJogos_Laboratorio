using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProjeto.Data;
using LabProjeto.Models;
using Microsoft.AspNetCore.Identity;

namespace LabProjeto.Controllers
{
    public class PerfilCategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public PerfilCategoriasController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: PerfilCategorias
        public async Task<IActionResult> Index()
        {
            string userId= null;
            foreach (var u in _context.Users)
            {
                if (u.UserName == User.Identity.Name)
                { 
                    userId=u.Id; break;
                }
            }
            var applicationDbContext = _context.PerfilCategoria.Where(p => p.perfil.utilizadorId == userId).ToListAsync();
            return View(await applicationDbContext);
        }

        // GET: PerfilCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PerfilCategoria == null)
            {
                return NotFound();
            }

            var perfilCategoria = await _context.PerfilCategoria
                .Include(p => p.categoria)
                .Include(p => p.perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilCategoria == null)
            {
                return NotFound();
            }

            return View(perfilCategoria);
        }

        // GET: PerfilCategorias/Create
        public IActionResult Create()
        {
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome");
            ViewData["perfilId"] = new SelectList(_context.PerfilModel, "Id", "utilizadorId");
            return View();
        }

        // POST: PerfilCategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,perfilId,categoriaId")] PerfilCategoria perfilCategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfilCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", perfilCategoria.categoriaId);
            ViewData["perfilId"] = new SelectList(_context.PerfilModel, "Id", "utilizadorId", perfilCategoria.perfilId);
            return View(perfilCategoria);
        }

        // GET: PerfilCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PerfilCategoria == null)
            {
                return NotFound();
            }

            var perfilCategoria = await _context.PerfilCategoria.FindAsync(id);
            if (perfilCategoria == null)
            {
                return NotFound();
            }
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", perfilCategoria.categoriaId);
            ViewData["perfilId"] = new SelectList(_context.PerfilModel, "Id", "utilizadorId", perfilCategoria.perfilId);
            return View(perfilCategoria);
        }

        // POST: PerfilCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,perfilId,categoriaId")] PerfilCategoria perfilCategoria)
        {
            if (id != perfilCategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilCategoriaExists(perfilCategoria.Id))
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
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", perfilCategoria.categoriaId);
            ViewData["perfilId"] = new SelectList(_context.PerfilModel, "Id", "utilizadorId", perfilCategoria.perfilId);
            return View(perfilCategoria);
        }

        // GET: PerfilCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PerfilCategoria == null)
            {
                return NotFound();
            }

            var perfilCategoria = await _context.PerfilCategoria
                .Include(p => p.categoria)
                .Include(p => p.perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilCategoria == null)
            {
                return NotFound();
            }

            return View(perfilCategoria);
        }

        // POST: PerfilCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PerfilCategoria == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PerfilCategoria'  is null.");
            }
            var perfilCategoria = await _context.PerfilCategoria.FindAsync(id);
            if (perfilCategoria != null)
            {
                _context.PerfilCategoria.Remove(perfilCategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilCategoriaExists(int id)
        {
          return _context.PerfilCategoria.Any(e => e.Id == id);
        }
    }
}
