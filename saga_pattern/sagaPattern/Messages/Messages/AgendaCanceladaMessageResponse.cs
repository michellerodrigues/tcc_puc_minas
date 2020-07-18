using System.Runtime.Serialization;

namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class AgendaCanceladaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaCancelada")]
        public AgendaMessage AgendaCancelada { get; set; } 
    }
}
