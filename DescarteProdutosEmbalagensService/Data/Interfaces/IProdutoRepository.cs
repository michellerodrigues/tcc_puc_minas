using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Data.Models;

namespace DescarteService.Data.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> FindProdutos(Func<Produto, bool> predicate);       
    }
}