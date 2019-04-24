using DescarteService.Services.Messages;

namespace DescarteService.Services
{
    public interface IDescarteApiService
    {
        ObterProdutosVencidosMessageResponse ObterProdutosVencidos();

        ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados();
    }
}