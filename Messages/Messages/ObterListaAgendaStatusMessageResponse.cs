using System.Collections.Generic;
using System.Runtime.Serialization;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class ObterListaAgendaStatusMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendasStatus")]
        public List<AgendaMessage> ListaAgendaStatus { get; set; } = new List<AgendaMessage>();

    }
}
