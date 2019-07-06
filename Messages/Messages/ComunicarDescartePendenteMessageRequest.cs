using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Messages.Services.Messages
{
    [DataContract]
    public class ComunicarDescartePendenteMessageRequest
    {      
        [DataMember(Name = "nomeResponsavel")]
        public string NomeResponsavel { get; set; } 

        [DataMember(Name = "emailRemetente")]
        public string EmailRemetente { get; set; } 

        [DataMember(Name = "datasDisponiveis")]
        public List<DatasDisponiveisMessage> DatasDisponiveis { get; set; } 

        [DataMember(Name = "listaProdutos")]
        public List<DescartePendente> ListaProdutos { get; set; } 

        [DataMember(Name = "nomeArquivo")]
        public string  NomeArquivo { get; set; } 

   }
}
