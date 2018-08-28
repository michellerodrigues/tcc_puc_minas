using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Data.Models;

namespace DescarteService.Data.Interfaces
{
    public interface IProdutoDescarteRepository : IRepository<ProdutoDescarte>
    {
        IEnumerable<ProdutoDescarte> FindProdutosVencidos();       
    }
}