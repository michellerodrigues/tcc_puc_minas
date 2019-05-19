using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Data.Models;

namespace DescarteService.Data.Interfaces
{
    public interface IAgendamentoDescarteRepository : IRepository<AgendamentoDescarteSolicitado>
    {
        IEnumerable<AgendamentoDescarteSolicitado> FindAgendamentoEmAndamento();

        IEnumerable<AgendamentoDescarteSolicitado> FindAgendamentoPendenteEnvioEmail();
    }

}