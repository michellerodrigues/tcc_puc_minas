using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AgendaService.Services.Messages;
using AgendaService.DataContext;
using Messages.Descartes.Events;
using AgendaService.Services;
using NServiceBus;
using Messages.Descartes.Commands;
using NServiceBus.Routing;
using AgendaService.Services.Interfaces;
using Microsoft.AspNetCore.Builder.Internal;


namespace AgendaService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AgendaController : Controller
    {     
        private readonly AppDataContext _context;
        private readonly IMessageSession _messageSession;
        public AgendaController(AppDataContext context, IMessageSession messageSession)
        {
            _context = context;

            _messageSession = messageSession;
        }

        [HttpGet]
        [Route("agendar")]
        public async Task<string> Agendar()
        {      
            AgendamentoMessage agendamento = new AgendamentoMessage(){};
            var message = new AgendarRetiradaCommand(){Id = Guid.NewGuid()};

            await _messageSession.Send(message).ConfigureAwait(false);
            
            return "Message sent to endpoint";
       }                   

        [HttpGet]
        [Route("ping")]
        public JsonResult Ping()
        {            
            return new JsonResult("pong");
        }                   

/* 
        [HttpGet]
        [Route("agendarRetirada/{email}/{id}")]
        public Task AgendarRetirada(string email, Guid id)
        {
           return _endpoint.SendLocal(new AgendarRetiradaCommand(){Id=id,EmailAgente=email,DataAgendamento=DateTime.Now.AddDays(15)});
        }
 */
        [HttpGet]
        [Route("cancelar")]
        public AgendaCanceladaMessageResponse CancelarAgenda(Guid Agenda)
        {
            AgendaCanceladaMessageResponse response = new AgendaCanceladaMessageResponse();

            IAgendaApiService agendaService = new AgendaApiService();

            response = agendaService.CancelarAgenda(Agenda);

            return response;
        }

        [HttpGet]
        [Route("confirmar/{Agenda}")]
        public AgendaConfirmadaMessageResponse ConfirmarAgenda(Guid Agenda)
        {
            AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.ConfirmarAgenda(Agenda);

            return response;
        }

        [HttpPost]
        [Route("finalizar/{Agenda}")]
        public AgendaFinalizadaMessageResponse ConfirmarRetiradaAgenda(Guid Agenda)
        {
            AgendaFinalizadaMessageResponse response = new AgendaFinalizadaMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.FinalizarAgenda(Agenda);

            return response;
        }

        [HttpGet]
        [Route("expirada")]
        public ObterAgendaExpiradaMessageResponse ObterAgendaExpirada()
        {
            ObterAgendaExpiradaMessageResponse response = new ObterAgendaExpiradaMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.ObterAgendaExpirada();

            return response;
        }

        [HttpGet]
        [Route("obter/status/{status}")]
        public ObterListaAgendaStatusMessageResponse ObterAgendasStatus(string status)
        {
            ObterListaAgendaStatusMessageResponse response = new ObterListaAgendaStatusMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.ObterAgendasPorStatus(status);

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

            AgendaApiService service = new AgendaApiService();

            response = service.ConfirmarAgenda(Agenda);

            return response;
        }
    }
}
