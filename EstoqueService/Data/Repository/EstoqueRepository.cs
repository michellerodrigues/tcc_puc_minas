using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.Data.Repository;

public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
    public EstoqueRepository(AppDataContext context) : base(context)
    {
    }

    public IEnumerable<Estoque> FindItensEstoque(Func<Estoque, bool> predicate)
    {
        return _context.Estoques.Where(predicate);
    }
}