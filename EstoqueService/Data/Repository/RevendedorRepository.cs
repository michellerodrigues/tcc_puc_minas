using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.Data.Repository;
using Microsoft.EntityFrameworkCore;

    public class RevendedorRepository : Repository<Revendedor>, IRevendedorRepository
    {
     public RevendedorRepository(DbSet<Revendedor> dbSet):base(dbSet){}
    

    public IEnumerable<Revendedor> FindRevendedor(Func<Revendedor, bool> predicate)
    {
        return _dbSet.Where(predicate);
    }
}