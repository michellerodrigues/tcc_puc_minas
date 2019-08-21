using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AgendaService.Data.Interfaces;
using AgendaService.Data.Models;
using AgendaService.DataContext;
using Messages.Descartes.Messages;
using Messages.Descartes.Commands;
using Messages.Descartes.Events;
using NServiceBus;

namespace AgendaService.Services.Interfaces
{
    public interface IAgendaApiService
    {
        Task<AgendamentoMessage> AgendarRetirada(Guid lote, string Data);
        AgendaCanceladaMessageResponse CancelarAgenda(Guid idAgenda);
        AgendaFinalizadaMessageResponse FinalizarAgenda(Guid idAgenda);
        ObterListaAgendaStatusMessageResponse ObterAgendasPorStatus(string status);
        ObterAgendaExpiradaMessageResponse ObterAgendaExpirada();
    }
}