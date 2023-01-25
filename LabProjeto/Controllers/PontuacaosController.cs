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
            if (!_context.Avalicao.Where(a=>a.JogoId==avalicao.JogoId).Any(a => a.Name == User.Identity.Name))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(avalicao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "JogoModels", new { id = avalicao.JogoId });
                }
            }
            else { 
                ModelState.AddModelError("Name", "Já existe uma avaliação sua"); 
            }
            
            ViewData["JogoId"] = id;
            return View(avalicao);
            
        }

        private bool AvalicaoExists(int id)
        {
          return _context.Avalicao.Any(e => e.Id == id);
        }
    }
}
