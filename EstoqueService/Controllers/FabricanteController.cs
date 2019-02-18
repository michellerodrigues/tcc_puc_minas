using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EstoqueService.Data.Models;
using EstoqueService.DataContext;

namespace EstoqueService.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly AppDataContext _context;

        public FabricanteController(AppDataContext context)
        {
            _context = context;
        }

        // GET: Fabricante
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fabricantes.ToListAsync());
        }

        // GET: Fabricante/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Fabricante = await _context.Fabricantes
                .SingleOrDefaultAsync(m => m.FabricanteId == id);
            if (Fabricante == null)
            {
                return NotFound();
            }

            return View(Fabricante);
        }

        // GET: Fabricante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabricante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FabricanteId,Nome,Email")] Fabricante Fabricante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Fabricante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Fabricante);
        }

        // GET: Fabricante/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Fabricante = await _context.Fabricantes.SingleOrDefaultAsync(m => m.FabricanteId == id);
            if (Fabricante == null)
            {
                return NotFound();
            }
            return View(Fabricante);
        }

        // POST: Fabricante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FabricanteId,Nome,Email")] Fabricante Fabricante)
        {
            if (id != Fabricante.FabricanteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Fabricante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricanteExists(Fabricante.FabricanteId))
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
            return View(Fabricante);
        }

        // GET: Fabricante/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Fabricante = await _context.Fabricantes
                .SingleOrDefaultAsync(m => m.FabricanteId == id);
            if (Fabricante == null)
            {
                return NotFound();
            }

            return View(Fabricante);
        }

        // POST: Fabricante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var Fabricante = await _context.Fabricantes.SingleOrDefaultAsync(m => m.FabricanteId == id);
            _context.Fabricantes.Remove(Fabricante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FabricanteExists(Guid id)
        {
            return _context.Fabricantes.Any(e => e.FabricanteId == id);
        }
    }
}
