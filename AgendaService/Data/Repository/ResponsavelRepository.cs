using System;
using System.Collections.Generic;
using System.Linq;
using AgendaService.DataContext;
using AgendaService.Data.Interfaces;
using AgendaService.Data.Models;
using AgendaService.Data.Repository;
using Microsoft.EntityFrameworkCore;

public class ResponsavelRepository : Repository<Responsavel>, IResponsavelRepository
{ 
    private new AppDataContext _context;
    public ResponsavelRepository(AppDataContext context):base(context)
    {
        _context = context;
        _context.Responsaveis.Include(t=>t.Agenda);
    }

    public IEnumerable<Responsavel> FindResponsavelByEmail(string email)
    {
         return _context.Responsaveis.Where(r=>r.Email.Equals(email)).ToList();
    }
}