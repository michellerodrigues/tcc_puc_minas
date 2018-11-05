using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.Data.Repository;
using Microsoft.EntityFrameworkCore;

public class FabricanteRepository : Repository<Fabricante>, IFabricanteRepository
    {
    public FabricanteRepository(AppDataContext context) : base(context)
    {
    }
    IEnumerable<Fabricante> IFabricanteRepository.FindFabricantes(Func<Fabricante, bool> predicate)
    {
         return _context.Fabricantes.Where(predicate);
    }
}