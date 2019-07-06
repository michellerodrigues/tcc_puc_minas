using System.Collections.Generic;
using System.Runtime.Serialization;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class AgendaCanceladaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaCancelada")]
        public AgendaMessage AgendaCancelada { get; set; } 
    }
}
