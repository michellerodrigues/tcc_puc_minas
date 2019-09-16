using System;
using System.Collections.Generic;
using System.Linq;
using DescarteService.DataContext;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;
using Microsoft.EntityFrameworkCore;

public class AgendamentoDescarteRepository : Repository<AgendamentoDescarteSolicitado>, IAgendamentoDescarteRepository
{
     private new AppDataContext _context = null;
    public AgendamentoDescarteRepository(AppDataContext context) : base(context)
    {
         this._context = context;
    }

    public IEnumerable<AgendamentoDescarteSolicitado> FindAgendamentoEmAndamento()
    {
         return this._context.AgendamentoDescarteSolicitados.Where(a=>a.DataEnvioEmail!=null && a.StatusProposta!="Cancelado" && a.StatusProposta!="Finalizado").Include(e=>e.LoteDescarte).ThenInclude(e=>e.ProdutosDescartes);;
    }

    public IEnumerable<AgendamentoDescarteSolicitado> FindAgendamentoPendenteEnvioEmail()
    {
         //pesquisar groupby
         return this._context.AgendamentoDescarteSolicitados.Where(a=> a.StatusProposta=="Pendente Envio Email").Include(e=>e.LoteDescarte).ThenInclude(e=>e.ProdutosDescartes).OrderBy(e=>e.LoteDescarteId).ToList();
    }

    public AgendamentoDescarteSolicitado FindAgendamentoEnviado(Guid lote, string data)
    {
         return this._context.AgendamentoDescarteSolicitados.Where(a=>a.Id.Equals(lote) && a.DataPropostaAgendamento==data && a.DataEnvioEmail!=null && a.StatusProposta=="Email Enviado").Include(e=>e.LoteDescarte).ThenInclude(e=>e.ProdutosDescartes).FirstOrDefault();
    }
    
    public IEnumerable<AgendamentoDescarteSolicitado> FindAgendamentoPorLote(Guid LoteId)
    {
         //pesquisar groupby
         return this._context.AgendamentoDescarteSolicitados.Where(a=>a.LoteDescarteId==LoteId).Include(e=>e.LoteDescarte).ThenInclude(e=>e.ProdutosDescartes).OrderBy(e=>e.LoteDescarteId).ToList();
    }

    public AgendamentoDescarteSolicitado FindAgendamentoPendenteEnvioEmail(Guid lote, string data)
    {
        throw new NotImplementedException();
    }
}