using Messages.Descartes.Messages;

namespace DescarteService.Services
{
    public interface IDescarteJobService
    {
        ObterProdutosVencidosMessageResponse ObterProdutosVencidos();

        ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados();
        
    }
}