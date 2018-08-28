using System;
using System.Collections.Generic;
using System.Linq;
using DescarteService.DataContext;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDataContext context) : base(context)
    {
    }

    public IEnumerable<Produto> FindProdutos(Func<Produto, bool> predicate)
    {
        return _context.Produtos.Where(predicate);
    }
}