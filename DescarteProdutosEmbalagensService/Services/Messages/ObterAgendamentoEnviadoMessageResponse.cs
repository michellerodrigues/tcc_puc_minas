using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DescarteService.Services.Messages
{
    [DataContract]
    public class ObterAgendamentoEnviadoMessageResponse:BaseResponseMessage
    {
      
        [DataMember(Name = "lote")]
        public Guid Lote{get;set;}

        [DataMember(Name = "dataProposta")]
        public string DataProposta{get;set;}

        [DataMember(Name = "dataEnvioEmail")]
        public DateTime DataEnvioEmail{get;set;}

        [DataMember(Name = "statusProposta")]
        public string StatusProposta{get;set;}

        [DataMember(Name = "nomeResponsavel")]
        public string NomeResponsavel{get;set;}

        [DataMember(Name = "emailResponsavel")]
        public string EmailResponsavel{get;set;}

    }
}
