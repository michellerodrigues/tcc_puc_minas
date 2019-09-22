using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AgendaService.DataContext;
using Messages.Descartes.Events;
using AgendaService.Services;
using NServiceBus;
using Messages.Descartes.Commands;
using Messages.Descartes.Messages;
using NServiceBus.Routing;
using AgendaService.Services.Interfaces;
using Microsoft.AspNetCore.Builder.Internal;
using AgendaService.Data.Interfaces;

namespace AgendaService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AgendaController : Controller
    {     
        private readonly IAgendaApiService _agendaApiService;
        private readonly IMessageSession _messageSession;
        public AgendaController(IAgendaApiService agendaApiService, IMessageSession messageSession)
        {
            _agendaApiService = agendaApiService;
            _messageSession = messageSession;
        }

        [HttpGet]
        [Route("ping")]
        public JsonResult Ping()
        {            
            return new JsonResult("pong");
        }                   



        [HttpGet]
        [Route("cancelar")]
        public AgendaCanceladaMessageResponse CancelarAgenda(Guid Agenda)
        {
            AgendaCanceladaMessageResponse response = new AgendaCanceladaMessageResponse();

            response = _agendaApiService.CancelarAgenda(Agenda);

            return response;
        }


        [HttpGet]
        [Route("expirada")]
        public ObterAgendaExpiradaMessageResponse ObterAgendaExpirada()
        {
            ObterAgendaExpiradaMessageResponse response = new ObterAgendaExpiradaMessageResponse();

            response = _agendaApiService.ObterAgendaExpirada();

            return response;
        }

        [HttpGet]
        [Route("obter/status/{status}")]
        public ObterListaAgendaStatusMessageResponse ObterAgendasStatus(string status)
        {
            ObterListaAgendaStatusMessageResponse response = new ObterListaAgendaStatusMessageResponse();

            response = _agendaApiService.ObterAgendasPorStatus(status);

            return response;
        }

        [HttpGet]
        [Route("confirmar/{Agenda}/{email}")]
        public async Task<AgendaConfirmadaMessageResponse> ConfirmarAgendamentoRetirada(Guid Agenda, string email)
        {
            /* AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "ok";
            response.AgendaConfirmada = new AgendaMessage(); 

            response = _agendaApiService.ConfirmarAgendamento(Agenda, email).GetAwaiter().GetResult();*/
            await _messageSession.SendLocal(
                new ConfirmarAgendamentoRetiradaCommand()
                {       
                    EmailSolicitacao = email,
                    Id = Agenda,
                    SolicitadoEm = DateTime.Now
                });

            AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Solicitação de Confrirmação Recebida. Por Favor, Aguarde o processamento";

            return response;
        }

        [HttpGet]
        [Route("agendar")]
        public async Task<AgendamentoMessage> AgendarRetirada(Guid lote, string data)
        {            
            AgendamentoMessage response = new AgendamentoMessage(); 
            response.codRetorno = 0;
            response.StatusRetorno = "Solicitação de Agendamento de Retirada Recebida. Por Favor, Aguarde o processamento";
            response.IdAgendamento = lote;
            await _messageSession.SendLocal(
                new AgendarRetiradaCommand()
                {       
                    DataAgendamento=data,
                    DataRegistro=DateTime.Now,
                    Id = lote,
                    LoteRetirada = lote.ToString()
                });
            return response;
        }
    }
}
