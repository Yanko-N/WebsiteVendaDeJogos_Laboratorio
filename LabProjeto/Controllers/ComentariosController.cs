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
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comentarios/Adicionar
        public IActionResult Adicionar(int? id)
        {

            if (_context.JogoModel.Any(j => j.Id == id))
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
        public async Task<IActionResult> Adicionar(int id, [Bind("Name,Message")] Comentario comentario)
        {
            comentario.JogoId = id;
            comentario.Jogo = _context.JogoModel.SingleOrDefault(j => j.Id == id);

            if (ModelState.IsValid)
            {
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","JogoModels", new { id = comentario.JogoId });
            }
            ViewData["JogoId"] = id;
            return View(comentario);
        }
    }
}
