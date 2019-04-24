using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EstoqueService.Services.Messages;
using EstoqueService.DataContext;
using EstoqueService.Services.Interfaces;

namespace EstoqueService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EstoqueController : Controller
    {     
        private readonly IEstoqueApiService _service;

        public EstoqueController(IEstoqueApiService service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("vencidos")]
        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
            ObterProdutosVencidosMessageResponse response = new ObterProdutosVencidosMessageResponse();

            response = _service.ObterProdutosVencidos();

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
