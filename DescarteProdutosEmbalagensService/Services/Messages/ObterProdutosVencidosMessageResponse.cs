using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DescarteService.Services.Messages
{
    [DataContract]
    public class ObterProdutosVencidosMessageResponse:BaseResponseMessage
    {
      
        [DataMember(Name = "loteProdutosVecidos")]
        public IEnumerable<ProdutoMessage> LoteProdutosVecidos { get; set; } = new List<ProdutoMessage>();
    }
}
