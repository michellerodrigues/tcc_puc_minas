using System.Collections.Generic;
using System.Runtime.Serialization;
using DescarteService.Services.Messages;

namespace DescarteService.Services.Messages
{
    [DataContract]
    public class ObterProdutosFinalizadosMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "loteProdutosFinalizados")]
        public IEnumerable<ProdutoMessage> LoteProdutosFinalizados { get; set; } = new List<ProdutoMessage>();
    }
}
