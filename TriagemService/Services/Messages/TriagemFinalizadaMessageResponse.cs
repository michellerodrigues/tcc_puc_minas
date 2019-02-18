using System.Collections.Generic;
using System.Runtime.Serialization;
using TriagemService.Services.Messages;

namespace TriagemService.Services.Messages
{
    [DataContract]
    public class TriagemFinalizadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "triagemFinalizada")]
        public TriagemMessage TriagemFinalizada { get; set; } 
    }
}
