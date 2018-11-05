using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EstoqueService.Services.Messages
{
    [DataContract]
    public class ObterProdutosVencidosMessageResponse:BaseResponseMessage
    {
      
        [DataMember(Name = "loteProdutosVecidos")]
        public List<ProdutoMessage> LoteProdutosVecidos { get; set; } = new List<ProdutoMessage>();
    }
}
