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
using Microsoft.AspNetCore.Identity;
using LabProjeto.Data.Migrations;

namespace LabProjeto.Controllers
{
    public class JogoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostEnvironment _he;

        private string _search;

        public JogoModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHostEnvironment he)
        {
            _context = context;
            _userManager = userManager;
            _he = he;
        }


        public async Task<IActionResult> HomeScreen()
        {

            var applicationDbContext = _context.JogoModel.Include(j => j.categoria);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JogoModels
        
        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.JogoModel
                .Include(j => j.categoria);

            string search = HttpContext.Session.GetString("myText");
            
            if (!string.IsNullOrEmpty(search))
            {
                HttpContext.Session.SetString("myText", "");


                var applicationDbContext2 = _context.JogoModel.Include(j => j.categoria).Where(j => j.Nome.Contains(search));

                if (applicationDbContext2.ToList().Count == 0)
                {
                    var applicationDbContext3 = _context.JogoModel.Include(j => j.categoria).Where(j => j.categoria.Nome.Contains(search));
                    if (applicationDbContext3.ToList().Count == 0)
                    {
                        var applicationDbContext4 = _context.JogoModel.Include(j => j.categoria).Where(j => j.plataforma.Contains(search));
                        return View(await applicationDbContext4.ToListAsync());
                    }
                    return View(await applicationDbContext3.ToListAsync());
                }

                return View(await applicationDbContext2.ToListAsync());

            }

            return View(await applicationDbContext.ToListAsync());
        }

        public  IActionResult Comprar(int id)
        {
            var jogo = _context.JogoModel.SingleOrDefault(j => j.Id == id);

            var user =  _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

            var perfil = _context.PerfilModel.SingleOrDefault(p=>p.utilizador == user);

            if (jogo != null && user != null)
            {
                PerfilJogos perfilJogos = new PerfilJogos()
                {
                    perfil = perfil,
                    perfilId = perfil.Id,
                    jogo=jogo,
                    jogoId=jogo.Id
                };

                _context.PerfilJogos.Add(perfilJogos);
                _context.SaveChangesAsync();

                return RedirectToAction("HomeScreen", "JogoModels");
            }
            else 
            {
                return NotFound();
            }


            
        }
        public async Task<IActionResult> VerJogos(string myText)
        {
            if (!string.IsNullOrEmpty(myText))
            {
                HttpContext.Session.SetString("myText", myText);
            }

            return RedirectToAction("Index", "JogoModels");
        }

        public async Task<IActionResult> VerCategorias(int? id)
        {
             var cat= _context.CategoriaModel.FirstOrDefault(m=>m.Id==id);
            if (cat != null)
            {
                HttpContext.Session.SetString("myText", cat.Nome);
            }

            return RedirectToAction("Index", "JogoModels");
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
        [Authorize(Roles = "Admin,Funcionario")]
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
        public async Task<IActionResult> Create([Bind("Nome,plataforma,Foto,Preco,categoriaId")] JogoModel jogoModel, IFormFile Foto)
        {

            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    string destination = Path.Combine(_he.ContentRootPath, "wwwroot/Fotos/Jogos/", Path.GetFileName(Foto.FileName));
                    FileStream fs = new FileStream(destination, FileMode.Create);
                    Foto.CopyTo(fs);
                    fs.Close();

                    jogoModel.Foto = Foto.FileName;
                }
                _context.Add(jogoModel);
                await _context.SaveChangesAsync();
                var jogo = _context.JogoModel.FirstOrDefault(j => j.Equals(jogoModel));
               
                return RedirectToAction(nameof(Index));
            }          
            ViewData["categoriaId"] = new SelectList(_context.CategoriaModel, "Id", "Nome", jogoModel.categoriaId);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,plataforma,Foto,categoriaId")] JogoModel jogoModel, IFormFile Foto)
        {
            if (id != jogoModel.Id)
            {
                return NotFound();
            }
            
            
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    string destination = Path.Combine(_he.ContentRootPath, "wwwroot/Fotos/Jogos/", Path.GetFileName(Foto.FileName));
                    FileStream fs = new FileStream(destination, FileMode.Create);
                    Foto.CopyTo(fs);
                    fs.Close();

                    jogoModel.Foto = Foto.FileName;
                }
                

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
