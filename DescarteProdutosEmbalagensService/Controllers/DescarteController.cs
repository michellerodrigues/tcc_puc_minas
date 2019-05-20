using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DescarteService.Services.Messages;
using DescarteService.DataContext;
using DescarteService.Services;

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
        [Route("notificacao")]
        public ObterAgendamentoEnviadoMessageResponse ObterNotificacaoDescarte(Guid lote, string data)
        {
            ObterAgendamentoEnviadoMessageResponse response = new ObterAgendamentoEnviadoMessageResponse();

            response = _service.ObterAgendamentoEnviado(lote, data);

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

    }
}
