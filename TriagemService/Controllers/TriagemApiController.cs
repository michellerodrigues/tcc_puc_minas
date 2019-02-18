using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TriagemService.Services.Messages;
using TriagemService.DataContext;

namespace TriagemService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TriagemApiController : ControllerBase
    {     
        private readonly AppDataContext _context;

        public TriagemApiController(AppDataContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("/cancelar")]
        public TriagemCanceladaMessageResponse CancelarTriagem(Guid triagem)
        {
            TriagemCanceladaMessageResponse response = new TriagemCanceladaMessageResponse();

            TriagemApiService service = new TriagemApiService();

            response = service.CancelarTriagem(triagem,_context);

            return response;
        }

        [HttpPost]
        [Route("/confirmar")]
        public TriagemConfirmadaMessageResponse ConfirmarTriagem(Guid triagem)
        {
            TriagemConfirmadaMessageResponse response = new TriagemConfirmadaMessageResponse();

            TriagemApiService service = new TriagemApiService();

            response = service.ConfirmarTriagem(triagem,_context);

            return response;
        }

        [HttpPost]
        [Route("/finalizar")]
        public TriagemFinalizadaMessageResponse ConfirmarRetiradaTriagem(Guid triagem)
        {
            TriagemFinalizadaMessageResponse response = new TriagemFinalizadaMessageResponse();

            TriagemApiService service = new TriagemApiService();

            response = service.FinalizarTriagem(triagem,_context);

            return response;
        }

        [HttpGet]
        [Route("/expirada")]
        public ObterTriagemExpiradaMessageResponse ObterTriagemExpirada()
        {
            ObterTriagemExpiradaMessageResponse response = new ObterTriagemExpiradaMessageResponse();

            TriagemApiService service = new TriagemApiService();

            response = service.ObterTriagemExpirada(_context);

            return response;
        }

        [HttpGet]
        [Route("/obter/status")]
        public ObterListaTriagemStatusMessageResponse ObterTriagensStatus(string status)
        {
            ObterListaTriagemStatusMessageResponse response = new ObterListaTriagemStatusMessageResponse();

            TriagemApiService service = new TriagemApiService();

            response = service.ObterTriagensPorStatus(status,_context);

            return response;
        }
    }
}
