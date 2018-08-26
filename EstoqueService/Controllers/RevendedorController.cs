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
    public class RevendedorController : Controller
    {
        private readonly AppDataContext _context;

        public RevendedorController(AppDataContext context)
        {
            _context = context;
        }

        // GET: Revendedor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Revendedores.ToListAsync());
        }

        // GET: Revendedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendedor = await _context.Revendedores
                .SingleOrDefaultAsync(m => m.RevendedorId == id);
            if (revendedor == null)
            {
                return NotFound();
            }

            return View(revendedor);
        }

        // GET: Revendedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Revendedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RevendedorId,Nome,Email")] Revendedor revendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(revendedor);
        }

        // GET: Revendedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendedor = await _context.Revendedores.SingleOrDefaultAsync(m => m.RevendedorId == id);
            if (revendedor == null)
            {
                return NotFound();
            }
            return View(revendedor);
        }

        // POST: Revendedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RevendedorId,Nome,Email")] Revendedor revendedor)
        {
            if (id != revendedor.RevendedorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevendedorExists(revendedor.RevendedorId))
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
            return View(revendedor);
        }

        // GET: Revendedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendedor = await _context.Revendedores
                .SingleOrDefaultAsync(m => m.RevendedorId == id);
            if (revendedor == null)
            {
                return NotFound();
            }

            return View(revendedor);
        }

        // POST: Revendedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revendedor = await _context.Revendedores.SingleOrDefaultAsync(m => m.RevendedorId == id);
            _context.Revendedores.Remove(revendedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevendedorExists(int id)
        {
            return _context.Revendedores.Any(e => e.RevendedorId == id);
        }
    }
}
