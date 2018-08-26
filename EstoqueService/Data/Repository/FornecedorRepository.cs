using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.Data.Repository;

public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
    public FornecedorRepository(AppDataContext context) : base(context)
    {
    }
    IEnumerable<Fornecedor> IFornecedorRepository.FindFornecedores(Func<Fornecedor, bool> predicate)
    {
        throw new NotImplementedException();
    }
}