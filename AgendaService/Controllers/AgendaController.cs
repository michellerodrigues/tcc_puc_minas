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


namespace AgendaService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AgendaController : Controller
    {     
        private readonly IAgendaApiService _agendaApiService;

        public AgendaController(IAgendaApiService agendaApiService)
        {
            _agendaApiService = agendaApiService;
        }

/*         [HttpGet]
        [Route("agendar")]
        public async Task Agendar(Guid lote, string Data)
        {   
            await _agendaApiService.AgendarRetirada(lote, Data);
        }                   
 */
        
        /* [HttpGet]
        [Route("obterAgendamento")]
        public ObterAgendamentoMessageResponse Agendar(Guid lote, string data)
        {   
            await _agendaApiService.VerificarAgendamentoSolicitado(agendamento);
        }  */               


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

            //IAgendaApiService agendaService = new AgendaApiService();

            response = _agendaApiService.CancelarAgenda(Agenda);

            return response;
        }

        [HttpGet]
        [Route("confirmar/{Agenda}")]
        public AgendaConfirmadaMessageResponse ConfirmarAgenda(Guid Agenda)
        {
            AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();

            //AgendaApiService service = new AgendaApiService();

         //   response = _agendaApiService.ConfirmarAgenda(Agenda);

            return response;
        }

        [HttpPost]
        [Route("finalizar/{Agenda}")]
        public AgendaFinalizadaMessageResponse ConfirmarRetiradaAgenda(Guid Agenda)
        {
            AgendaFinalizadaMessageResponse response = new AgendaFinalizadaMessageResponse();

            //AgendaApiService service = new AgendaApiService();

            response = _agendaApiService.FinalizarAgenda(Agenda);

            return response;
        }

        [HttpGet]
        [Route("expirada")]
        public ObterAgendaExpiradaMessageResponse ObterAgendaExpirada()
        {
            ObterAgendaExpiradaMessageResponse response = new ObterAgendaExpiradaMessageResponse();

            //AgendaApiService service = new AgendaApiService();

            response = _agendaApiService.ObterAgendaExpirada();

            return response;
        }

        [HttpGet]
        [Route("obter/status/{status}")]
        public ObterListaAgendaStatusMessageResponse ObterAgendasStatus(string status)
        {
            ObterListaAgendaStatusMessageResponse response = new ObterListaAgendaStatusMessageResponse();

            //AgendaApiService service = new AgendaApiService();

            response = _agendaApiService.ObterAgendasPorStatus(status);

            return response;
        }

        [HttpGet]
        [Route("confirmar/{Agenda}/{email}")]
        public AgendaConfirmadaMessageResponse ConfirmarAgendamentoRetirada(Guid Agenda, string email)
        {


            AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "ok";
            response.AgendaConfirmada = new AgendaMessage();

            //AgendaApiService service = new AgendaApiService();

            //response = _agendaApiService.ConfirmarAgenda(Agenda);

            return response;
        }

        [HttpGet]
        [Route("agendar")]
        public AgendamentoMessage AgendarRetirada(Guid lote, string data)
        {
            AgendamentoMessage response = new AgendamentoMessage(); 

            //AgendaApiService service = new AgendaApiService();

            response = _agendaApiService.AgendarRetirada(lote, data).GetAwaiter().GetResult();
            
            return response;
        }

    }
}
