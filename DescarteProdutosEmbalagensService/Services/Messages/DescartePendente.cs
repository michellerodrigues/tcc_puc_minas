using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DescarteService.Services.Messages
{
    [DataContract]
    public class DescartePendente
    {
        [DataMember(Name = "idItemEstoque")]
        public int IdItemEstoque { get; set; }

        [DataMember(Name = "nomeProduto")]
        public string NomeProduto { get; set; }
        
        [DataMember(Name = "dataVencimento")]
        public string DataVencimento { get; set; }

        [DataMember(Name = "qtdeLiquidaProdutoDisponivel")]
        public string QtdeprodutoDisponivel { get; set; }
    }
}
