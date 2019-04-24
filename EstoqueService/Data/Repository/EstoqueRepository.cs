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

public class EstoqueRepository : IUnitOfWork<Estoque>
{
    private readonly IUnitOfWork _uow;

    public EstoqueRepository()
    {
    }

    public EstoqueRepository(IUnitOfWork unit )
    {
        _uow = unit;
    }

    public Estoque Context => throw new NotImplementedException();

    public int Commit()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Estoque> FindItensEstoque(Estoque entity)
    {
        return _uow.GetRepository<Estoque>().Get();
    }

    public IEnumerable<Estoque> FindItensFinalizadosEstoque()
    {
         return _context.Estoques.Include(e=>e.Revendedor).Include(e=>e.Fabricante).Include(e=>e.Produto).Where(e=>e.Descartado==false && e.QtdeDispUnidade<=0).OrderBy(e=>e.Revendedor);
    }

    public IEnumerable<Estoque> FindItensVencidosEstoque()
    {
        return _uow.GetRepository<Estoque>().(e=>e.Revendedor).Include(e=>e.Fabricante).Include(e=>e.Produto).Where(e=>e.Descartado==false && e.DataVecimentoProduto.ToOADate()<=DateTime.Now.ToOADate() && e.QtdeDispUnidade>0).OrderBy(e=>e.Fabricante);
        return _context.Estoques.Include(e=>e.Revendedor).Include(e=>e.Fabricante).Include(e=>e.Produto).Where(e=>e.Descartado==false && e.DataVecimentoProduto.ToOADate()<=DateTime.Now.ToOADate() && e.QtdeDispUnidade>0).OrderBy(e=>e.Fabricante);
    }

    public IEnumerable<Estoque> Get()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Estoque> Get(Expression<Func<Estoque, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        throw new NotImplementedException();
    }

    public void Update(Estoque entity)
    {
        throw new NotImplementedException();
    }
}