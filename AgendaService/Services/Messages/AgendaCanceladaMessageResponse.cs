using System.Collections.Generic;
using System.Runtime.Serialization;
using AgendaService.Services.Messages;

namespace AgendaService.Services.Messages
{
    [DataContract]
    public class AgendaCanceladaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaCancelada")]
        public AgendaMessage AgendaCancelada { get; set; } 
    }
}
