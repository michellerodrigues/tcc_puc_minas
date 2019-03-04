using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AgendaService.Services.Messages;

namespace AgendaService.Services.Messages
{
    [DataContract]
    public class AgendamentoMessage: BaseResponseMessage
    {
        [DataMember(Name = "IdAgendamento")]
        public Guid IdAgendamento { get; set; } 

        [DataMember(Name = "Email")]
        public string Email { get; set; } 


    }
}
