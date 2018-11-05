using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EstoqueService.Data.JoinFacade;

namespace EstoqueService.Data.Models
{
    public class Fabricante
    {
        [Key]
        public int FabricanteId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }
     
        public virtual ICollection<Estoque> Estoques { get; } = new List<Estoque>();
    }
}
