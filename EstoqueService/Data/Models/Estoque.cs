using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EstoqueService.Data.JoinFacade;

namespace EstoqueService.Data.Models
{
    public class Estoque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Lote { get; set; }

        [Required]
        [StringLength(3)]
        public string Serie { get; set; }

        [Required]
        public DateTime DataInclusao { get; set; }

        [Required]
        public DateTime DataVecimentoProduto { get; set; }

        [Required]
        public decimal QtdeDispUnidade { get; set; }

        [Required]
        public bool Descartado { get; set; }

        [Required]
        public Fornecedor Fornecedor { get; set;} 

        [Required]
        public Revendedor Revendedor { get; set;} 

        [Required]
        public Produto Produto { get; set;} 
    }
}
