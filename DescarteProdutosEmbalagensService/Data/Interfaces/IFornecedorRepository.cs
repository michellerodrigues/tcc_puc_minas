using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Data.Models;

namespace DescarteService.Data.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        IEnumerable<Fornecedor> FindFornecedores(Func<Fornecedor, bool> predicate);
    }
}