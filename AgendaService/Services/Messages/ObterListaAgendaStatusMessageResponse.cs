using System.Collections.Generic;
using System.Runtime.Serialization;
using AgendaService.Services.Messages;

namespace AgendaService.Services.Messages
{
    [DataContract]
    public class ObterListaAgendaStatusMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendasStatus")]
        public List<AgendaMessage> ListaAgendaStatus { get; set; } = new List<AgendaMessage>();

    }
}
