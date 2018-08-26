using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EstoqueService.Data.JoinFacade;

namespace EstoqueService.Data.Models
{
    public class Fornecedor
    {
        public Fornecedor()
        {
            Produtos = new JoinCollectionFacade<Produto, Fornecedor, FornecedorProduto>(this, FornecedorProdutos);
        } 

        [Key]
        public int FornecedorId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        private ICollection<FornecedorProduto> FornecedorProdutos { get; } = new List<FornecedorProduto>();

        [NotMapped]        
        private ICollection<Produto> Produtos { get; } = new List<Produto>();



    }
}
