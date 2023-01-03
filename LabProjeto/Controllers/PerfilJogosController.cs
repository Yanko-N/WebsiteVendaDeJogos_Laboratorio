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
    public class PerfilJogosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfilJogosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PerfilJogos
        public async Task<IActionResult> Index()
        {
              return View(await _context.PerfilJogos.ToListAsync());
        }

        // GET: PerfilJogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PerfilJogos == null)
            {
                return NotFound();
            }

            var perfilJogos = await _context.PerfilJogos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilJogos == null)
            {
                return NotFound();
            }

            return View(perfilJogos);
        }

        // GET: PerfilJogos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PerfilJogos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,perfilId,jogoId")] PerfilJogos perfilJogos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfilJogos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(perfilJogos);
        }

        // GET: PerfilJogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PerfilJogos == null)
            {
                return NotFound();
            }

            var perfilJogos = await _context.PerfilJogos.FindAsync(id);
            if (perfilJogos == null)
            {
                return NotFound();
            }
            return View(perfilJogos);
        }

        // POST: PerfilJogos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,perfilId,jogoId")] PerfilJogos perfilJogos)
        {
            if (id != perfilJogos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilJogos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilJogosExists(perfilJogos.Id))
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
            return View(perfilJogos);
        }

        // GET: PerfilJogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PerfilJogos == null)
            {
                return NotFound();
            }

            var perfilJogos = await _context.PerfilJogos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilJogos == null)
            {
                return NotFound();
            }

            return View(perfilJogos);
        }

        // POST: PerfilJogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {   
            
            if (_context.PerfilJogos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PerfilJogos'  is null.");
            }
            var perfilJogos = await _context.PerfilJogos.FindAsync(id);
            if (perfilJogos != null)
            {
                _context.PerfilJogos.Remove(perfilJogos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilJogosExists(int id)
        {
          return _context.PerfilJogos.Any(e => e.Id == id);
        }
    }
}
