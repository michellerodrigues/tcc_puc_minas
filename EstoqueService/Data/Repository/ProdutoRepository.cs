using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.Data.Repository;
using Microsoft.EntityFrameworkCore;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(DbSet<Produto> dbSet) : base(dbSet) 
    { 
        dbSet.Include(e=>e.Estoques);
    }

    public IEnumerable<Produto> FindProdutos(Func<Produto, bool> predicate)
    {
        throw new NotImplementedException();
    }
}
