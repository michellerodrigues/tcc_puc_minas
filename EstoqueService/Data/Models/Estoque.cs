using System;
using System.ComponentModel.DataAnnotations;

namespace EstoqueService.Data.Models
{
    public class Estoque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public Produto IdProduto { get; set; }

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
        public Revendedor RevendidoPor { get; set; }

        [Required]
        public Fornecedor FornecidoPor { get; set; }

        [Required]
        public bool Descartado { get; set; }
    }
}
