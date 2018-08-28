using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DescarteService.Data.JoinFacade;

namespace DescarteService.Data.Models
{
    public class Produto
    {
        public Produto()
        {
            Revendedores = new JoinCollectionFacade<Revendedor,Produto, RevendedorProduto>(this, RevendedorProdutos);
            Fornecedores = new JoinCollectionFacade<Fornecedor, Produto, FornecedorProduto>(this, FornecedorProdutos);
        } 


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public decimal PesoCheio { get; set; }

        [Required]
        public decimal PesoVazio { get; set; }

        [Required]
        public decimal VolumeEmbalagem { get; set; }

        [Required]        
        private ICollection<RevendedorProduto> RevendedorProdutos { get; } = new List<RevendedorProduto>();
        
        [NotMapped]
        public ICollection<Revendedor> Revendedores { get; }


        [Required]        
        private ICollection<FornecedorProduto> FornecedorProdutos { get; } = new List<FornecedorProduto>();
        
        [NotMapped]
        public ICollection<Fornecedor> Fornecedores { get; }

    }
}
