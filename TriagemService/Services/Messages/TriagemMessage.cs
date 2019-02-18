using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TriagemService.Services.Messages
{
    [DataContract]
    public class TriagemMessage
    {
        [DataMember(Name = "idTriagem")]
        public Guid IdTriagem { get; set; }

        [DataMember(Name = "dataCriacao")]
        public DateTime DataCriacao { get; set; }

        [DataMember(Name = "loteTriagem")]
        public string LoteDescarte { get; set; }

        [DataMember(Name = "dataStatus")]
        public DateTime DataStatus { get; set; }        
        
        [DataMember(Name = "statusTriagem")]
        public string StatusTriagem { get; set; }

        [DataMember(Name = "nomeOperadorResponsavel")]
        public string Operador { get; set;} 

        [DataMember(Name = "emailOperador")]
        public string EmailOperador { get; set;} 
    }
}
