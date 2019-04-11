using System;
using System.Collections.Generic;
using System.Linq;
using DescarteService.DataContext;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;

public class LoteDescarteRepository : Repository<LoteDescarte>, ILoteDescarteRepository
{
    public LoteDescarteRepository(AppDataContext context) : base(context)
    {
        string mica = "";
    }

    public IEnumerable<LoteDescarte> FindLoteDescartePorResponsavel(string nomeResponsavel)
    {
          return _context.LoteDescartes.Where(l=>l.NomeResponsavelDescarte.Equals(nomeResponsavel));
    }

}