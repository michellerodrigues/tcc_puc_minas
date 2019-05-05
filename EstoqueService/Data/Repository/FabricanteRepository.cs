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
    public FabricanteRepository(DbSet<Fabricante> dbSet):base(dbSet){}

    IEnumerable<Fabricante> IFabricanteRepository.FindFabricantes(Func<Fabricante, bool> predicate)
    {
         return _dbSet.Where(predicate);
    }
}