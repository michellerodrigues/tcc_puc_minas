using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EstoqueService.Data.JoinFacade;

namespace EstoqueService.Data.Models
{
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [NotMapped]        
        private ICollection<Estoque> ItensEmEstoque { get; } = new List<Estoque>();

    }
}
