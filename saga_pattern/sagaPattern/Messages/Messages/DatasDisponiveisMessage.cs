using System;
using System.Runtime.Serialization;

namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class DatasDisponiveisMessage
    {
        [DataMember(Name = "data")]
        public DateTime Data { get; set; }

        [DataMember(Name = "linkAgendamento")]
        public string LinkAgendamento { get; set; }
    }
}
