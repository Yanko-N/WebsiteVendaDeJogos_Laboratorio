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
using Microsoft.AspNetCore.Http;

namespace LabProjeto.Controllers
{
    public class JogoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string _search;

        public JogoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        // GET: JogoModels
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JogoModel.Include(j => j.categoria);
            string search = HttpContext.Session.GetString("myText");
            
            if (!string.IsNullOrEmpty(search))
            {
                HttpContext.Session.SetString("myText", "");


                var applicationDbContext2 = _context.JogoModel.Include(j => j.categoria).Where(j => j.Nome.Contains(search));

                if (applicationDbContext2.ToList().Count == 0)
                {
                    var applicationDbContext3 = _context.JogoModel.Include(j => j.categoria).Where(j => j.categoria.Nome.Contains(search));
                    return View(await applicationDbContext3.ToListAsync());
                }

                return View(await applicationDbContext2.ToListAsync());

            }

            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult VerJogos(string myText)
        {
            if (!string.IsNullOrEmpty(myText))
            {
                HttpContext.Session.SetString("myText", myText);
            }

            return RedirectToAction("Index", "JogoModels");
        }


        // GET: JogoModels/Details/5
        [Authorize(Roles = "Admin,Funcionario")]
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
        [Authorize(Roles = "Admin,Funcionario")]
        public IActionResult Create()
        {
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome");
            ViewData["plataformaId"] = new SelectList(_context.PlataformaModel, "Id", "Nome");
            return View();
        }

        // POST: JogoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Admin,Funcionario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,plataformaId,categoriaId")] JogoModel jogoModel)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(jogoModel);
                await _context.SaveChangesAsync();

                var jogo = _context.JogoModel.FirstOrDefault(j => j.Equals(jogoModel));
                JogoCategoria jc = new JogoCategoria();
                if (jogo != null)
                {
                    jc.jogoId = jogoModel.Id;
                    jc.categoriaId = jogoModel.categoriaId;
                    _context.Add(jc);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
           
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoModel.categoriaId);
            ViewData["plataformaId"] = new SelectList(_context.PlataformaModel, "Id", "Nome", jogoModel.plataformaId);
            return View(jogoModel);
        }

        // GET: JogoModels/Edit/5
        [Authorize(Roles = "Admin,Funcionario")]
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
        [Authorize(Roles = "Admin,Funcionario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,plataformaID,categoriaId")] JogoModel jogoModel)
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

                var JogoCategoria = _context.JogoCategoria.SingleOrDefault(c => c.jogoId == jogoModel.Id);
                if (JogoCategoria != null)
                {
                    JogoCategoria.categoria = jogoModel.categoria;
                    JogoCategoria.categoriaId = jogoModel.categoriaId;


                    _context.Update(JogoCategoria);
                    await _context.SaveChangesAsync();

                }


                //  _context.JogoCategoria.Where(j => j.jogoId = jogoModel.Id) = JogoCategoria;
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoModel.categoriaId);
            return View(jogoModel);
        }

        // GET: JogoModels/Delete/5
        [Authorize(Roles = "Admin,Funcionario")]
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
        [Authorize(Roles = "Admin,Funcionario")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            //REMOVER JOGO DE JOGO CATEGORIA

            var jogo= _context.JogoCategoria.Single(o => o .jogoId == id);
            if (jogo != null)
            {
                _context.JogoCategoria.Remove(jogo);
                await _context.SaveChangesAsync();
            }


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
