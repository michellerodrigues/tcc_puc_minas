using System.Collections.Generic;
using System.Runtime.Serialization;
using Agropop.Descarte.Messages;

namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class ObterProdutosFinalizadosMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "loteProdutosFinalizados")]
        public IEnumerable<ProdutoMessage> LoteProdutosFinalizados { get; set; } = new List<ProdutoMessage>();

        [DataMember(Name = "datasOfertadas")]
        public List<string> DatasOfertadas { get; set; } = new List<string>();
    }
}
