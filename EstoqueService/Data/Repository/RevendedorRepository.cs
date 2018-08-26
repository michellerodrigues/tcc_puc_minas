using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.Data.Repository;

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