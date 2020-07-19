using System.Runtime.Serialization;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class AgendaCanceladaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaCancelada")]
        public AgendaMessage AgendaCancelada { get; set; } 
    }
}
