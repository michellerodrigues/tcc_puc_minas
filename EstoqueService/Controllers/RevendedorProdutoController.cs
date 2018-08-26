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
    public class RevendedorProdutoController : Controller
    {
        private readonly AppDataContext _context;

        public RevendedorProdutoController(AppDataContext context)
        {
            _context = context;
        }

        // GET: RevendedorProduto
        public async Task<IActionResult> Index()
        {
            var appDataContext = _context.RevendedorProduto.Include(r => r.Produto).Include(r => r.Revendedor);
            return View(await appDataContext.ToListAsync());
        }

        // GET: RevendedorProduto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendedorProduto = await _context.RevendedorProduto
                .Include(r => r.Produto)
                .Include(r => r.Revendedor)
                .SingleOrDefaultAsync(m => m.RevendedorId == id);
            if (revendedorProduto == null)
            {
                return NotFound();
            }

            return View(revendedorProduto);
        }

        // GET: RevendedorProduto/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome");
            ViewData["RevendedorId"] = new SelectList(_context.Revendedores, "RevendedorId", "Email");
            return View();
        }

        // POST: RevendedorProduto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RevendedorId,ProdutoId")] RevendedorProduto revendedorProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revendedorProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", revendedorProduto.ProdutoId);
            ViewData["RevendedorId"] = new SelectList(_context.Revendedores, "RevendedorId", "Email", revendedorProduto.RevendedorId);
            return View(revendedorProduto);
        }

        // GET: RevendedorProduto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendedorProduto = await _context.RevendedorProduto.SingleOrDefaultAsync(m => m.RevendedorId == id);
            if (revendedorProduto == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", revendedorProduto.ProdutoId);
            ViewData["RevendedorId"] = new SelectList(_context.Revendedores, "RevendedorId", "Email", revendedorProduto.RevendedorId);
            return View(revendedorProduto);
        }

        // POST: RevendedorProduto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RevendedorId,ProdutoId")] RevendedorProduto revendedorProduto)
        {
            if (id != revendedorProduto.RevendedorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revendedorProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevendedorProdutoExists(revendedorProduto.RevendedorId))
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
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", revendedorProduto.ProdutoId);
            ViewData["RevendedorId"] = new SelectList(_context.Revendedores, "RevendedorId", "Email", revendedorProduto.RevendedorId);
            return View(revendedorProduto);
        }

        // GET: RevendedorProduto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendedorProduto = await _context.RevendedorProduto
                .Include(r => r.Produto)
                .Include(r => r.Revendedor)
                .SingleOrDefaultAsync(m => m.RevendedorId == id);
            if (revendedorProduto == null)
            {
                return NotFound();
            }

            return View(revendedorProduto);
        }

        // POST: RevendedorProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revendedorProduto = await _context.RevendedorProduto.SingleOrDefaultAsync(m => m.RevendedorId == id);
            _context.RevendedorProduto.Remove(revendedorProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevendedorProdutoExists(int id)
        {
            return _context.RevendedorProduto.Any(e => e.RevendedorId == id);
        }
    }
}
