using Agropop.Saga;
using Agropop.Saga.Interfaces;
using Messages.Descartes.Messages;


namespace DescarteService.Services
{
    public class DescarteJobService : MessageHandlerContext, IDescarteJobService
    {
        private readonly DescarteSaga _descarteSaga;

        public DescarteJobService(IChannel channel) : base(channel)
        {
            _descarteSaga = new DescarteSaga(channel);
            _descarteSaga.Consume();
        }

        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados()
        {
            //vai no banco, verifica se tem produto vencido
            //transforma a mensagem para a saga correta

           //transforma em emnsagem base

           //envia pra fila


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
