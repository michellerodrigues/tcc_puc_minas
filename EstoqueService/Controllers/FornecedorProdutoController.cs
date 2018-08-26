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
    public class FornecedorProdutoController : Controller
    {
        private readonly AppDataContext _context;

        public FornecedorProdutoController(AppDataContext context)
        {
            _context = context;
        }

        // GET: FornecedorProduto
        public async Task<IActionResult> Index()
        {
            var appDataContext = _context.FornecedorProduto.Include(f => f.Fornecedor).Include(f => f.Produto);
            return View(await appDataContext.ToListAsync());
        }

        // GET: FornecedorProduto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorProduto = await _context.FornecedorProduto
                .Include(f => f.Fornecedor)
                .Include(f => f.Produto)
                .SingleOrDefaultAsync(m => m.FornecedorId == id);
            if (fornecedorProduto == null)
            {
                return NotFound();
            }

            return View(fornecedorProduto);
        }

        // GET: FornecedorProduto/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "FornecedorId", "Email");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome");
            return View();
        }

        // POST: FornecedorProduto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FornecedorId,ProdutoId")] FornecedorProduto fornecedorProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fornecedorProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "FornecedorId", "Email", fornecedorProduto.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", fornecedorProduto.ProdutoId);
            return View(fornecedorProduto);
        }

        // GET: FornecedorProduto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorProduto = await _context.FornecedorProduto.SingleOrDefaultAsync(m => m.FornecedorId == id);
            if (fornecedorProduto == null)
            {
                return NotFound();
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "FornecedorId", "Email", fornecedorProduto.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", fornecedorProduto.ProdutoId);
            return View(fornecedorProduto);
        }

        // POST: FornecedorProduto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FornecedorId,ProdutoId")] FornecedorProduto fornecedorProduto)
        {
            if (id != fornecedorProduto.FornecedorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedorProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorProdutoExists(fornecedorProduto.FornecedorId))
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
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "FornecedorId", "Email", fornecedorProduto.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", fornecedorProduto.ProdutoId);
            return View(fornecedorProduto);
        }

        // GET: FornecedorProduto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorProduto = await _context.FornecedorProduto
                .Include(f => f.Fornecedor)
                .Include(f => f.Produto)
                .SingleOrDefaultAsync(m => m.FornecedorId == id);
            if (fornecedorProduto == null)
            {
                return NotFound();
            }

            return View(fornecedorProduto);
        }

        // POST: FornecedorProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedorProduto = await _context.FornecedorProduto.SingleOrDefaultAsync(m => m.FornecedorId == id);
            _context.FornecedorProduto.Remove(fornecedorProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorProdutoExists(int id)
        {
            return _context.FornecedorProduto.Any(e => e.FornecedorId == id);
        }
    }
}
