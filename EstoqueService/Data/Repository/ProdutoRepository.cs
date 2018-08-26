using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.Data.Repository;

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