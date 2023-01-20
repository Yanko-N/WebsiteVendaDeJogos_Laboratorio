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
    public class PontuacaosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PontuacaosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pontuacaos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pontuacao.Include(p => p.Jogo).Include(p => p.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pontuacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pontuacao == null)
            {
                return NotFound();
            }

            var pontuacao = await _context.Pontuacao
                .Include(p => p.Jogo)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontuacao == null)
            {
                return NotFound();
            }

            return View(pontuacao);
        }

        // GET: Pontuacaos/Create
        public IActionResult Create()
        {
            ViewData["JogoId"] = new SelectList(_context.JogoModel, "Id", "Nome"); 
            
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Pontuacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,utilizadorId,pontuacao,JogoId")] Pontuacao pontuacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pontuacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JogoId"] = new SelectList(_context.JogoModel, "Id", "Nome", pontuacao.JogoId);
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id", pontuacao.utilizadorId);
            return View(pontuacao);
        }

        // GET: Pontuacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pontuacao == null)
            {
                return NotFound();
            }

            var pontuacao = await _context.Pontuacao.FindAsync(id);
            if (pontuacao == null)
            {
                return NotFound();
            }
            ViewData["JogoId"] = new SelectList(_context.JogoModel, "Id", "Nome", pontuacao.JogoId);
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id", pontuacao.utilizadorId);
            return View(pontuacao);
        }

        // POST: Pontuacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,utilizadorId,pontuacao,JogoId")] Pontuacao pontuacao)
        {
            if (id != pontuacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pontuacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontuacaoExists(pontuacao.Id))
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
            ViewData["JogoId"] = new SelectList(_context.JogoModel, "Id", "Nome", pontuacao.JogoId);
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id", pontuacao.utilizadorId);
            return View(pontuacao);
        }

        // GET: Pontuacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pontuacao == null)
            {
                return NotFound();
            }

            var pontuacao = await _context.Pontuacao
                .Include(p => p.Jogo)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontuacao == null)
            {
                return NotFound();
            }

            return View(pontuacao);
        }

        // POST: Pontuacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pontuacao == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pontuacao'  is null.");
            }
            var pontuacao = await _context.Pontuacao.FindAsync(id);
            if (pontuacao != null)
            {
                _context.Pontuacao.Remove(pontuacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontuacaoExists(int id)
        {
          return _context.Pontuacao.Any(e => e.Id == id);
        }
    }
}
