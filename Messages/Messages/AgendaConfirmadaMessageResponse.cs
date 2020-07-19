using System.Runtime.Serialization;


namespace Messages.Descartes.Messages
{
    [DataContract]
    public class AgendaConfirmadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaConfirmada")]
        public AgendaMessage AgendaConfirmada { get; set; }
    }
}
