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
    public class AgendamentoDescarteController : Controller
    {
        private readonly AppDataContext _context;

        public AgendamentoDescarteController(AppDataContext context)
        {
            _context = context;
        }

        // GET: AgendamentoDescarte
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgendamentoDescartes.ToListAsync());
        }

        // GET: AgendamentoDescarte/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamentoDescarte = await _context.AgendamentoDescartes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (agendamentoDescarte == null)
            {
                return NotFound();
            }

            return View(agendamentoDescarte);
        }

        // GET: AgendamentoDescarte/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgendamentoDescarte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataEnvioEmail,DataPropostaAgendamento,StatusProposta")] ComunicadosDeAgendamentoEnviados agendamentoDescarte)
        {

            if (ModelState.IsValid)
            {
                _context.Add(agendamentoDescarte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agendamentoDescarte);
        }

        // GET: AgendamentoDescarte/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamentoDescarte = await _context.AgendamentoDescartes.SingleOrDefaultAsync(m => m.Id == id);
            if (agendamentoDescarte == null)
            {
                return NotFound();
            }
            return View(agendamentoDescarte);
        }

        // POST: AgendamentoDescarte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DataEnvioEmail,DataPropostaAgendamento,StatusProposta")] ComunicadosDeAgendamentoEnviados agendamentoDescarte)
        {
            if (id != agendamentoDescarte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamentoDescarte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoDescarteExists(agendamentoDescarte.Id))
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
            return View(agendamentoDescarte);
        }

        // GET: AgendamentoDescarte/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamentoDescarte = await _context.AgendamentoDescartes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (agendamentoDescarte == null)
            {
                return NotFound();
            }

            return View(agendamentoDescarte);
        }

        // POST: AgendamentoDescarte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agendamentoDescarte = await _context.AgendamentoDescartes.SingleOrDefaultAsync(m => m.Id == id);
            _context.AgendamentoDescartes.Remove(agendamentoDescarte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoDescarteExists(Guid id)
        {
            return _context.AgendamentoDescartes.Any(e => e.Id == id);
        }
    }
}
