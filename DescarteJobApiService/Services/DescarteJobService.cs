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
            //lote encontrado deverá ser passado como informaçaõ do descarte
            string lote = "1234";
            //transforma a mensagem para a saga correta

            DescarteSagaCreateMessage createMessage = new DescarteSagaCreateMessage();

            var mensagemFila = createMessage.TransformCommandToMessage(lote);
           //transforma em emnsagem base
           //envia pra fila

            var mensagem = new ObterProdutosFinalizadosMessageResponse()
            {
                codRetorno=0,
                StatusRetorno = "Solicitação de ObterPodutosFinalizados enviados para a fila com sucesso"
            };;

            base.Publish(mensagemFila);
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
