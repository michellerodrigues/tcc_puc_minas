using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class AgendamentoMessage: BaseResponseMessage
    {
        [DataMember(Name = "IdAgendamento")]
        public Guid IdAgendamento { get; set; } 

        [DataMember(Name = "Email")]
        public string Email { get; set; } 

        [DataMember(Name = "DataRegistro")]
        public DateTime DataRegistro { get; set; }  = DateTime.Now.AddDays(15);
    }
}
