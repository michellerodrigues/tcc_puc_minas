using System;
using System.Collections.Generic;
using System.Linq;
using AgendaService.DataContext;
using AgendaService.Data.Interfaces;
using AgendaService.Data.Models;
using AgendaService.Data.Repository;
using Microsoft.EntityFrameworkCore;

public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
    public AgendaRepository(AppDataContext context) : base(context)
    {
        _context.Agendas.Include(t=>t.Responsavel);
    }

    public IEnumerable<Agenda> FindItensAgenda(Func<Agenda, bool> predicate)
    {
        return _context.Agendas.Where(predicate);
    }

    public IEnumerable<Agenda> FindAgendaExpirada()
    {
         return _context.Agendas.Include(t=>t.Responsavel).Where(t=>t.DataExpiracao>=DateTime.Now && t.Verificada==false);
    }

    public IEnumerable<Agenda> FindAgendaStatus(string status)
    {
         return _context.Agendas.Include(t=>t.Responsavel).Where(t=>t.StatusAgenda==status && t.DataExpiracao<DateTime.Now);
    }
}