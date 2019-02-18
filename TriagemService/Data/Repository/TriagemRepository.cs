using System;
using System.Collections.Generic;
using System.Linq;
using TriagemService.DataContext;
using TriagemService.Data.Interfaces;
using TriagemService.Data.Models;
using TriagemService.Data.Repository;
using Microsoft.EntityFrameworkCore;

public class TriagemRepository : Repository<Triagem>, ITriagemRepository
    {
    public TriagemRepository(AppDataContext context) : base(context)
    {
        _context.Triagens.Include(t=>t.Operador);
    }

    public IEnumerable<Triagem> FindItensTriagem(Func<Triagem, bool> predicate)
    {
        return _context.Triagens.Where(predicate);
    }

    public IEnumerable<Triagem> FindTriagemExpirada()
    {
         return _context.Triagens.Include(t=>t.Operador).Where(t=>t.DataExpiracao>=DateTime.Now && t.Verificada==false);
    }

    public IEnumerable<Triagem> FindTriagemStatus(string status)
    {
         return _context.Triagens.Include(t=>t.Operador).Where(t=>t.StatusTriagem==status && t.DataExpiracao<DateTime.Now);
    }
}