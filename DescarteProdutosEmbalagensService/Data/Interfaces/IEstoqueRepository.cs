using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Data.Models;

namespace DescarteService.Data.Interfaces
{
    public interface IEstoqueRepository : IRepository<Estoque>
    {
        IEnumerable<Estoque> FindItensEstoque(Func<Estoque, bool> predicate);

        IEnumerable<Estoque> FindItensVencidosEstoque();

        IEnumerable<Estoque> FindItensFinalizadosEstoque();
    }

}