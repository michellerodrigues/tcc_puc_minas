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

namespace AgendaService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AgendaApiController : ControllerBase
    {     
        private readonly AppDataContext _context;

        public AgendaApiController(AppDataContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("/cancelar")]
        public AgendaCanceladaMessageResponse CancelarAgenda(Guid Agenda)
        {
            AgendaCanceladaMessageResponse response = new AgendaCanceladaMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.CancelarAgenda(Agenda,_context);

            return response;
        }

        [HttpGet]
        [Route("/confirmar/{0}")]
        public AgendaConfirmadaMessageResponse ConfirmarAgenda(Guid Agenda)
        {
            AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.ConfirmarAgenda(Agenda,_context);

            return response;
        }

        [HttpPost]
        [Route("/finalizar")]
        public AgendaFinalizadaMessageResponse ConfirmarRetiradaAgenda(Guid Agenda)
        {
            AgendaFinalizadaMessageResponse response = new AgendaFinalizadaMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.FinalizarAgenda(Agenda,_context);

            return response;
        }

        [HttpGet]
        [Route("/expirada")]
        public ObterAgendaExpiradaMessageResponse ObterAgendaExpirada()
        {
            ObterAgendaExpiradaMessageResponse response = new ObterAgendaExpiradaMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.ObterAgendaExpirada(_context);

            return response;
        }

        [HttpGet]
        [Route("/obter/status")]
        public ObterListaAgendaStatusMessageResponse ObterAgendasStatus(string status)
        {
            ObterListaAgendaStatusMessageResponse response = new ObterListaAgendaStatusMessageResponse();

            AgendaApiService service = new AgendaApiService();

            response = service.ObterAgendasPorStatus(status,_context);

            return response;
        }

        [HttpGet]
        [Route("/confirmar")]
        public AgendaConfirmadaMessageResponse ConfirmarAgendamentoRetirada(Guid Agenda, string email)
        {


            AgendaConfirmadaMessageResponse response = new AgendaConfirmadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "ok";
            response.AgendaConfirmada = new AgendaMessage();

            AgendaApiService service = new AgendaApiService();

            response = service.ConfirmarAgenda(Agenda,_context);

            return response;
        }
    }
}
