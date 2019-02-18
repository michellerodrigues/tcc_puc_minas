using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaService.Data.Models;

namespace AgendaService.Data.Interfaces
{
    public interface IAgendaRepository : IRepository<Agenda>
    {
        IEnumerable<Agenda> FindItensAgenda(Func<Agenda, bool> predicate);

        IEnumerable<Agenda> FindAgendaExpirada();

        IEnumerable<Agenda> FindAgendaStatus(string status);
    }

}