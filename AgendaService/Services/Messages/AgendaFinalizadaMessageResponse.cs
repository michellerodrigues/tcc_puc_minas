using System.Collections.Generic;
using System.Runtime.Serialization;
using AgendaService.Services.Messages;

namespace AgendaService.Services.Messages
{
    [DataContract]
    public class AgendaFinalizadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaFinalizada")]
        public AgendaMessage AgendaFinalizada { get; set; } 
    }
}
