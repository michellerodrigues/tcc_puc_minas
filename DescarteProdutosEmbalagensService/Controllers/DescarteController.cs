using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Messages.Descartes.Messages;
using DescarteService.DataContext;
using DescarteService.Services;
using DescarteService.Data.Models;

namespace DescarteService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DescarteController : Controller
    {     
        private readonly IDescarteApiService _service;

        public DescarteController(IDescarteApiService service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("agendamento/enviado")]
        public ObterAgendamentoMessageResponse ObterNotificacaoDescarte(Guid lote, string data)
        {
            ObterAgendamentoMessageResponse response = new ObterAgendamentoMessageResponse();

            response = _service.ObterAgendamentoEnviado(lote, data);

            return response;
        }

        [HttpGet]
        [Route("agendamento/pendente")]
        public ObterAgendamentoPendenteMessageResponse ObterNotificacaoDescartePendente()
        {
            ObterAgendamentoPendenteMessageResponse response = new ObterAgendamentoPendenteMessageResponse();

            response = _service.ObterAgendamentoPendente();

            return response;
        }


        [HttpGet]
        [Route("finalizados")]
        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados()
        {
            ObterProdutosFinalizadosMessageResponse response = new ObterProdutosFinalizadosMessageResponse();

            response = _service.ObterProdutosFinalizados();

            return response;
        }

        
        [HttpGet]
        [Route("vencidos")]
        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
            ObterProdutosVencidosMessageResponse response = new ObterProdutosVencidosMessageResponse();

            response = _service.ObterProdutosVencidos();

            return response;
        }

    }
}
