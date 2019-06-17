using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DescarteService.Services.Messages
{
    [DataContract]
    public class ObterAgendamentoPendenteMessageResponse:BaseResponseMessage
    {      
        [DataMember(Name = "listaAgendamentoPendenteEnvioEmail")]
        public List<ObterAgendamentoMessageResponse> listaPendencias{get;set;}    
    }
}
