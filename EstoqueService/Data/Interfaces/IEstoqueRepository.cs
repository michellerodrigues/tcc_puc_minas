using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueService.Data.Models;

namespace EstoqueService.Data.Interfaces
{
    public interface IEstoqueRepository : IRepository<Estoque>
    {
        IEnumerable<Estoque> FindItensEstoque(Func<Estoque, bool> predicate);
    }
}