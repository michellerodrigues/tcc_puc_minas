using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class ObterProdutosFinalizadosMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "loteProdutosFinalizados")]
        public IEnumerable<ProdutoMessage> LoteProdutosFinalizados { get; set; } = new List<ProdutoMessage>();
    }
}
