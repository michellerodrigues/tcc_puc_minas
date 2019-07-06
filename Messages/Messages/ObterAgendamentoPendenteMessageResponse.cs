using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Messages.Descartes.Messages
{
    [DataContract]
    public class ObterAgendamentoPendenteMessageResponse:BaseResponseMessage
    {      
        [DataMember(Name = "listaAgendamentoPendenteEnvioEmail")]
        public List<ObterAgendamentoMessageResponse> listaPendencias{get;set;}    
    }
}
