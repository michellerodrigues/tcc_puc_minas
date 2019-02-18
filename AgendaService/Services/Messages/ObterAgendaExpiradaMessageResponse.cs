using System.Collections.Generic;
using System.Runtime.Serialization;
using AgendaService.Services.Messages;

namespace AgendaService.Services.Messages
{
    [DataContract]
    public class ObterAgendaExpiradaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendasExpiradas")]
        public List<AgendaMessage> ListaAgendasExpiradas { get; set; } = new List<AgendaMessage>();
    }
}
