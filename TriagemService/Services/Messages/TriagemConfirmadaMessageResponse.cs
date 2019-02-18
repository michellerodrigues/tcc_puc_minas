using System.Collections.Generic;
using System.Runtime.Serialization;
using TriagemService.Services.Messages;

namespace TriagemService.Services.Messages
{
    [DataContract]
    public class TriagemConfirmadaMessageResponse: BaseResponseMessage
    {
        [DataMember(Name = "triagemConfirmada")]
        public TriagemMessage TriagemConfirmada { get; set; }
    }
}
