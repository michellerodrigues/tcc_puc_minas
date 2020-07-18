using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class ObterListaAgendaStatusMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendasStatus")]
        public List<AgendaMessage> ListaAgendaStatus { get; set; } = new List<AgendaMessage>();

    }
}
