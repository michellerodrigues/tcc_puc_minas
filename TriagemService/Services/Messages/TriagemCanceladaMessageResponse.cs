using System.Collections.Generic;
using System.Runtime.Serialization;
using TriagemService.Services.Messages;

namespace TriagemService.Services.Messages
{
    [DataContract]
    public class TriagemCanceladaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "triagemCancelada")]
        public TriagemMessage TriagemCancelada { get; set; } 
    }
}
