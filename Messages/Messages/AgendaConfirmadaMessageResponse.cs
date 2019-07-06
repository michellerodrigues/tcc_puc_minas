using System.Collections.Generic;
using System.Runtime.Serialization;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class AgendaConfirmadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaConfirmada")]
        public AgendaMessage AgendaConfirmada { get; set; }
    }
}
