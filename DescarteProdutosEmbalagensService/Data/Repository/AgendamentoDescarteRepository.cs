using System;
using System.Collections.Generic;
using System.Linq;
using DescarteService.DataContext;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;

public class AgendamentoDescarteRepository : Repository<AgendamentoDescarte>, IAgendamentoDescarteRepository
    {
    public AgendamentoDescarteRepository(AppDataContext context) : base(context)
    {
       
    }

    public IEnumerable<AgendamentoDescarte> FindAgendamentoEmAndamento()
    {
         return _context.AgendamentoDescartes.Where(a=>a.DataAgendamento!=null && a.StatusAgendamento!="Cancelado" && a.StatusAgendamento!="Finalizado");
    }

}