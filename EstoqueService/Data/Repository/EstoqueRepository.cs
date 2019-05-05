using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EstoqueService.Services.Util;

public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
{
    public EstoqueRepository(DbSet<Estoque> dbSet):base(dbSet)
    {
    }
    
    public IEnumerable<Estoque> FindItensFinalizadosEstoque()
    {
        return _dbSet.Where(e=>e.Descartado==false && e.QtdeDispUnidade<=0).OrderBy(e=>e.Revendedor).AsEnumerable();
    }

    public IEnumerable<Estoque> FindItensVencidosEstoque()
    {
        return _dbSet.Where(e=>e.Descartado==false && e.DataVecimentoProduto.ToOADate()<=DateTime.Now.ToOADate() && e.QtdeDispUnidade>0).OrderBy(e=>e.Fabricante);
    }
}