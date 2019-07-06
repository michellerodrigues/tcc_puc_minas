using System.Collections.Generic;
using System.Runtime.Serialization;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class AgendaFinalizadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaFinalizada")]
        public AgendaMessage AgendaFinalizada { get; set; } 
    }
}
