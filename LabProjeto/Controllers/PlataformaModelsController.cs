﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProjeto.Data;
using LabProjeto.Models;
using Microsoft.AspNetCore.Authorization;

namespace LabProjeto.Controllers
{
    public class PlataformaModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlataformaModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlataformaModels
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.PlataformaModel.ToListAsync());
        }

        // GET: PlataformaModels/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlataformaModel == null)
            {
                return NotFound();
            }

            var plataformaModel = await _context.PlataformaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plataformaModel == null)
            {
                return NotFound();
            }

            return View(plataformaModel);
        }

        // GET: PlataformaModels/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlataformaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] PlataformaModel plataformaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plataformaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plataformaModel);
        }

        // GET: PlataformaModels/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlataformaModel == null)
            {
                return NotFound();
            }

            var plataformaModel = await _context.PlataformaModel.FindAsync(id);
            if (plataformaModel == null)
            {
                return NotFound();
            }
            return View(plataformaModel);
        }

        // POST: PlataformaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] PlataformaModel plataformaModel)
        {
            if (id != plataformaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plataformaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlataformaModelExists(plataformaModel.Id))
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
            return View(plataformaModel);
        }

        // GET: PlataformaModels/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlataformaModel == null)
            {
                return NotFound();
            }

            var plataformaModel = await _context.PlataformaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plataformaModel == null)
            {
                return NotFound();
            }

            return View(plataformaModel);
        }

        // POST: PlataformaModels/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlataformaModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PlataformaModel'  is null.");
            }
            var plataformaModel = await _context.PlataformaModel.FindAsync(id);
            if (plataformaModel != null)
            {
                _context.PlataformaModel.Remove(plataformaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlataformaModelExists(int id)
        {
          return _context.PlataformaModel.Any(e => e.Id == id);
        }
    }
}