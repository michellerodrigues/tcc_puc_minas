using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EstoqueService.Services.Messages;
using Messages.Descartes.Commands;
using Messages.Descartes.Events;
using NServiceBus;

namespace EstoqueService.Services.Interfaces
{
    public interface IEstoqueApiService
    {
        ObterProdutosVencidosMessageResponse ObterProdutosVencidos();        
        ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados();
    }
}