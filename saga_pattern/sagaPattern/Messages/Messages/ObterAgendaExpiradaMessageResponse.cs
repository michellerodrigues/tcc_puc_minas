using System.Collections.Generic;
using System.Runtime.Serialization;
using Messages.Descartes.Messages;

namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class ObterAgendaExpiradaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendasExpiradas")]
        public List<AgendaMessage> ListaAgendasExpiradas { get; set; } = new List<AgendaMessage>();
    }
}
