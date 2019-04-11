using System;
using System.Collections.Generic;
using System.Linq;
using DescarteService.DataContext;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;

public class AgendamentoDescarteRepository : Repository<ComunicadosDeAgendamentoEnviados>, IAgendamentoDescarteRepository
    {
    public AgendamentoDescarteRepository(AppDataContext context) : base(context)
    {
       
    }

    public IEnumerable<ComunicadosDeAgendamentoEnviados> FindAgendamentoEmAndamento()
    {
         return _context.AgendamentoDescartes.Where(a=>a.DataEnvioEmail!=null && a.StatusProposta!="Cancelado" && a.StatusProposta!="Finalizado");
    }

}