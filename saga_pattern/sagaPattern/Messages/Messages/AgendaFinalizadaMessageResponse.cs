
using System.Runtime.Serialization;


namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class AgendaFinalizadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "AgendaFinalizada")]
        public AgendaMessage AgendaFinalizada { get; set; } 
    }
}
