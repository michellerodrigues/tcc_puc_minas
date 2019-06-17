using System;
using System.Collections.Generic;
using DescarteService.Data.Models;
using DescarteService.Services.Messages;

namespace DescarteService.Services
{
    public interface IDescarteApiService
    {
        ObterProdutosVencidosMessageResponse ObterProdutosVencidos();

        ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados();
    
        ObterAgendamentoMessageResponse ObterAgendamentoEnviado(Guid lote, string Data);

        ObterAgendamentoPendenteMessageResponse ObterAgendamentoPendente();        
    }
}