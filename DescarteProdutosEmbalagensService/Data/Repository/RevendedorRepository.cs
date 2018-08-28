using System;
using System.Collections.Generic;
using System.Linq;
using DescarteService.DataContext;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;

public class RevendedorRepository : Repository<Revendedor>, IRevendedorRepository
    {
    public RevendedorRepository(AppDataContext context) : base(context)
    {
    }

    public IEnumerable<Revendedor> FindRevendedor(Func<Revendedor, bool> predicate)
    {
        return _context.Revendedores.Where(predicate);
    }
}