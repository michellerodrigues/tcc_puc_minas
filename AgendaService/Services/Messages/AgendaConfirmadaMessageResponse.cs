using System.Collections.Generic;
using System.Runtime.Serialization;
using AgendaService.Services.Messages;

namespace AgendaService.Services.Messages
{
    [DataContract]
    public class AgendaConfirmadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaConfirmada")]
        public AgendaMessage AgendaConfirmada { get; set; }
    }
}
