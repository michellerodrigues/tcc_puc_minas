using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EstoqueService.Data.JoinFacade;

namespace EstoqueService.Data.Models
{
    public class Estoque
    {
        [Key]
        public Guid Id { get; set; }

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
       
        [ForeignKey("FabricanteId")]
        public virtual Fabricante Fabricante { get; set;} 

        [ForeignKey("RevendedorId")]
        public virtual Revendedor Revendedor { get; set;} 
        
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set;} 
    }
}
