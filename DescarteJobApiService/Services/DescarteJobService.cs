using Agropop.Saga;
using Messages.Descartes.Messages;


namespace DescarteService.Services
{
    public class DescarteJobService : MessageHandlerContext, IDescarteJobService
    {
        public DescarteJobService(IChannel channel) : base(channel)
        {
        }

        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados()
        {
            var mensagem = new ObterProdutosFinalizadosMessageResponse()
            {
                codRetorno=0,
                StatusRetorno = "Solicitação de ObterPodutosFinalizados enviados para a fila com sucesso"
            };;

            base.Publish(mensagem);
            //publicar na fila do descarte: "ObterProdutosFinalizadosCommand"

            return mensagem;
        }
        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
            //publicar na fila do descarte: "ObterProdutosFinalizadosCommand"

            return new ObterProdutosVencidosMessageResponse()
            {
                codRetorno=0,
                StatusRetorno = "Solicitação de ObterPodutosVencidsos enviados para a fila com sucesso"
            };
        }
        
    }
}
