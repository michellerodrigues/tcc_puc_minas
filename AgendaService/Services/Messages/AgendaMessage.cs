using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AgendaService.Services.Messages
{
    [DataContract]
    public class AgendaMessage
    {
        [DataMember(Name = "idAgenda")]
        public Guid IdAgenda { get; set; }

        [DataMember(Name = "dataCriacao")]
        public DateTime DataCriacao { get; set; }

        [DataMember(Name = "loteAgenda")]
        public string LoteDescarte { get; set; }

        [DataMember(Name = "dataStatus")]
        public DateTime DataStatus { get; set; }        
        
        [DataMember(Name = "statusAgenda")]
        public string StatusAgenda { get; set; }

        [DataMember(Name = "nomeResponsavel")]
        public string Responsavel { get; set;} 

        [DataMember(Name = "emailResponsavel")]
        public string EmailResponsavel { get; set;} 
    }
}
