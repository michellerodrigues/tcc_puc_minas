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
    public class EstoqueApiController : ControllerBase
    {     
        private readonly AppDataContext _context;

        public EstoqueApiController(AppDataContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("/vencidos")]
        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
            ObterProdutosVencidosMessageResponse response = new ObterProdutosVencidosMessageResponse();

            DescarteApiService service = new DescarteApiService(_context);

            response = service.ObterProdutosVencidos();

            return response;
        }

        [HttpGet]
        [Route("/finalizados")]
        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados()
        {
            ObterProdutosFinalizadosMessageResponse response = new ObterProdutosFinalizadosMessageResponse();

            DescarteApiService service = new DescarteApiService(_context);

            response = service.ObterProdutosFinalizados();

            return response;
        }

    }
}
