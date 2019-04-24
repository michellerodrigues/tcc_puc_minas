using System.Collections.Generic;
using System.Runtime.Serialization;
using EstoqueService.Services.Messages;

namespace EstoqueService.Services.Messages
{
    [DataContract]
    public class ObterProdutosFinalizadosMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "loteProdutosFinalizados")]
        public List<ProdutoMessage> LoteProdutosFinalizados { get; set; } = new List<ProdutoMessage>();
    }
}
