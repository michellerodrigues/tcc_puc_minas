using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EstoqueService.Services.Messages
{
    [DataContract]
    public class ProdutoMessage
    {
        [DataMember(Name = "idItemEstoque")]
        public int IdItemEstoque { get; set; }

        [DataMember(Name = "nomeProduto")]
        public string NomeProduto { get; set; }
        
        [DataMember(Name = "dataVencimento")]
        public string DataVencimento { get; set; }

        [DataMember(Name = "qtdeLiquidaProdutoDisponivel")]
        public string QtdeprodutoDisponivel { get; set; }

        [DataMember(Name = "emailFabricante")]
        public string EmailFabricante { get; set; }

        [DataMember(Name = "emailRevendedor")]
        public string EmailRevendedor { get; set; }

        [DataMember(Name = "nomeResponsavel")]
        public string NomeResponsavel { get; set; }

        [DataMember(Name = "pesoCheio")]
        public decimal PesoCheio { get; set; }

        [DataMember(Name = "pesoVazio")]
        public decimal PesoVazio { get; set; }

        [DataMember(Name = "volumeEmbalagem")]
        public decimal VolumeEmbalagem { get; set; }
    }
}
