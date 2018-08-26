using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EstoqueService.Data.JoinFacade;

namespace EstoqueService.Data.Models
{
    public class Revendedor
    {
        public Revendedor()
        {
            Produtos = new JoinCollectionFacade<Produto, Revendedor, RevendedorProduto>(this, RevendedorProdutos);
        } 
        [Key]
        public int RevendedorId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }
     
        private ICollection<RevendedorProduto> RevendedorProdutos { get; } = new List<RevendedorProduto>();

        [NotMapped]        
        private ICollection<Produto> Produtos { get; } = new List<Produto>();

    }
}
