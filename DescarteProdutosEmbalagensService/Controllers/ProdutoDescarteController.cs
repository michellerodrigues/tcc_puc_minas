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
    public class ProdutoDescarteController : Controller
    {
        private readonly AppDataContext _context;

        public ProdutoDescarteController(AppDataContext context)
        {
            _context = context;
        }

        // GET: ProdutoDescarte
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProdutoDescartes.ToListAsync());
        }

        // GET: ProdutoDescarte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoDescarte = await _context.ProdutoDescartes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (produtoDescarte == null)
            {
                return NotFound();
            }

            return View(produtoDescarte);
        }

        // GET: ProdutoDescarte/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProdutoDescarte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,IdITemEstoque,PesoCheio,PesoVazio,VolumeEmbalagem,DataVecimentoProduto")] ProdutoDescarte produtoDescarte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtoDescarte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtoDescarte);
        }

        // GET: ProdutoDescarte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoDescarte = await _context.ProdutoDescartes.SingleOrDefaultAsync(m => m.Id == id);
            if (produtoDescarte == null)
            {
                return NotFound();
            }
            return View(produtoDescarte);
        }

        // POST: ProdutoDescarte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,IdITemEstoque,PesoCheio,PesoVazio,VolumeEmbalagem,DataVecimentoProduto")] ProdutoDescarte produtoDescarte)
        {
            if (id != produtoDescarte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoDescarte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoDescarteExists(produtoDescarte.Id))
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
            return View(produtoDescarte);
        }

        // GET: ProdutoDescarte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoDescarte = await _context.ProdutoDescartes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (produtoDescarte == null)
            {
                return NotFound();
            }

            return View(produtoDescarte);
        }

        // POST: ProdutoDescarte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtoDescarte = await _context.ProdutoDescartes.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProdutoDescartes.Remove(produtoDescarte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoDescarteExists(int id)
        {
            return _context.ProdutoDescartes.Any(e => e.Id == id);
        }
    }
}
