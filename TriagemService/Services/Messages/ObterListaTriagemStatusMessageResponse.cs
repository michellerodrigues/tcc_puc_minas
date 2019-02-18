using System.Collections.Generic;
using System.Runtime.Serialization;
using TriagemService.Services.Messages;

namespace TriagemService.Services.Messages
{
    [DataContract]
    public class ObterListaTriagemStatusMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "triagensStatus")]
        public List<TriagemMessage> ListaTriagemStatus { get; set; } = new List<TriagemMessage>();

    }
}
