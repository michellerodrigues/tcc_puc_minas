using System.Collections.Generic;
using System.Runtime.Serialization;
using TriagemService.Services.Messages;

namespace TriagemService.Services.Messages
{
    [DataContract]
    public class ObterTriagemExpiradaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "triagensExpiradas")]
        public List<TriagemMessage> ListaTriagensExpiradas { get; set; } = new List<TriagemMessage>();
    }
}
