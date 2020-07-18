using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class ObterProdutosVencidosMessageResponse:BaseResponseMessage
    {
      
        [DataMember(Name = "loteProdutosVecidos")]
        public IEnumerable<ProdutoMessage> LoteProdutosVecidos { get; set; } = new List<ProdutoMessage>();

        [DataMember(Name = "datasOfertadas")]
        public List<string> DatasOfertadas { get; set; } = new List<string>();
    }
}
