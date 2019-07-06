using System.Collections.Generic;
using System.Runtime.Serialization;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class ObterAgendaExpiradaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendasExpiradas")]
        public List<AgendaMessage> ListaAgendasExpiradas { get; set; } = new List<AgendaMessage>();
    }
}
