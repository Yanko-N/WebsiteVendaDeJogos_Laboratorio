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

        public IActionResult Adicionar(int? id)
        {

            if (_context.JogoModel.Any(j => j.Id == id) && User.Identity.IsAuthenticated)
            {
                ViewData["JogoId"] = id;
                return View();
            }
            else
            {
                return NotFound();
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(int id, [Bind("Name,pontuacao")] Avalicao avalicao)
        {
            avalicao.JogoId = id;
            avalicao.Jogo = _context.JogoModel.SingleOrDefault(j => j.Id == id);

            if (ModelState.IsValid)
            {
                _context.Add(avalicao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "JogoModels", new { id = avalicao.JogoId });
            }
            ViewData["JogoId"] = id;
            return View(avalicao);
        }

        // GET: Pontuacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Avalicao == null)
            {
                return NotFound();
            }

            var avalicao = await _context.Avalicao.FindAsync(id);
            if (avalicao == null)
            {
                return NotFound();
            }
            ViewData["JogoId"] = new SelectList(_context.JogoModel, "Id", "Nome", avalicao.JogoId);
            return View(avalicao);
        }

        // POST: Pontuacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JogoId,Name,pontuacao")] Avalicao avalicao)
        {
            if (id != avalicao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avalicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvalicaoExists(avalicao.Id))
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
            ViewData["JogoId"] = new SelectList(_context.JogoModel, "Id", "Nome", avalicao.JogoId);
            return View(avalicao);
        }

        
        private bool AvalicaoExists(int id)
        {
          return _context.Avalicao.Any(e => e.Id == id);
        }
    }
}
