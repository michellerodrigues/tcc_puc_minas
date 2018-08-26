using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueService.Data.Models;

namespace EstoqueService.Data.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        IEnumerable<Fornecedor> FindFornecedores(Func<Fornecedor, bool> predicate);
    }
}