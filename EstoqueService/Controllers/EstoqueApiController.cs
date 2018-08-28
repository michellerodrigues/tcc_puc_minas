using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EstoqueService.Services.Messages;
using EstoqueService.DataContext;

namespace EstoqueService.Controllers
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

            EstoqueApiService service = new EstoqueApiService();

            response = service.ObterProdutosVencidos(_context);

            return response;
        }

        [HttpGet]
        [Route("/finalizados")]
        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados()
        {
            ObterProdutosFinalizadosMessageResponse response = new ObterProdutosFinalizadosMessageResponse();

            EstoqueApiService service = new EstoqueApiService();

            response = service.ObterProdutosFinalizados(_context);

            return response;
        }

    }
}
