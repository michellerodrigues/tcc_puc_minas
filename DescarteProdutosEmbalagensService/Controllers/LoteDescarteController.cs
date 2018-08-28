using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DescarteService.Data.Models;
using DescarteService.DataContext;

namespace DescarteService.Controllers
{
    public class LoteDescarteController : Controller
    {
        private readonly AppDataContext _context;

        public LoteDescarteController(AppDataContext context)
        {
            _context = context;
        }

        // GET: LoteDescarte
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoteDescartes.ToListAsync());
        }

        // GET: LoteDescarte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteDescarte = await _context.LoteDescartes
                .SingleOrDefaultAsync(m => m.LoteDescarteId == id);
            if (loteDescarte == null)
            {
                return NotFound();
            }

            return View(loteDescarte);
        }

        // GET: LoteDescarte/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoteDescarte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoteDescarteId,NomeResponsavelDescarte,EmailResponsavelDescarte")] LoteDescarte loteDescarte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loteDescarte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loteDescarte);
        }

        // GET: LoteDescarte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteDescarte = await _context.LoteDescartes.SingleOrDefaultAsync(m => m.LoteDescarteId == id);
            if (loteDescarte == null)
            {
                return NotFound();
            }
            return View(loteDescarte);
        }

        // POST: LoteDescarte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoteDescarteId,NomeResponsavelDescarte,EmailResponsavelDescarte")] LoteDescarte loteDescarte)
        {
            if (id != loteDescarte.LoteDescarteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loteDescarte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoteDescarteExists(loteDescarte.LoteDescarteId))
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
            return View(loteDescarte);
        }

        // GET: LoteDescarte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteDescarte = await _context.LoteDescartes
                .SingleOrDefaultAsync(m => m.LoteDescarteId == id);
            if (loteDescarte == null)
            {
                return NotFound();
            }

            return View(loteDescarte);
        }

        // POST: LoteDescarte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loteDescarte = await _context.LoteDescartes.SingleOrDefaultAsync(m => m.LoteDescarteId == id);
            _context.LoteDescartes.Remove(loteDescarte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoteDescarteExists(int id)
        {
            return _context.LoteDescartes.Any(e => e.LoteDescarteId == id);
        }
    }
}
