using System.Runtime.Serialization;


namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class AgendaConfirmadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaConfirmada")]
        public AgendaMessage AgendaConfirmada { get; set; }
    }
}
