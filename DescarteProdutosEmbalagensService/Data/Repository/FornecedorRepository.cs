using System;
using System.Collections.Generic;
using System.Linq;
using DescarteService.DataContext;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;

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