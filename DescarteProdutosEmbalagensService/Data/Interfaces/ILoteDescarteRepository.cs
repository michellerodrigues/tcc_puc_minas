using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Data.Models;

namespace DescarteService.Data.Interfaces
{
    public interface ILoteDescarteRepository : IRepository<LoteDescarte>
    {
        IEnumerable<LoteDescarte> FindLoteDescartePorResponsavel(string nomeResponsavel);
    }
}