using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Agropop.Descarte.Messages;

namespace Agropop.Descarte.Messages
{
    [DataContract]
    public class ObterAgendamentoPendenteMessageResponse:BaseResponseMessage
    {      
        [DataMember(Name = "listaAgendamentoPendenteEnvioEmail")]
        public List<ObterAgendamentoMessageResponse> listaPendencias{get;set;}    
    }
}
