using Microsoft.AspNetCore.Mvc;
using Messages.Descartes.Messages;
using DescarteService.Services;

namespace DescarteService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DescarteApiController : Controller
    {     
        private readonly IDescarteJobService _service;

        public DescarteApiController(IDescarteJobService service)
        {
            _service = service;
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
