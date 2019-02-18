using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriagemService.Data.Models
{
    public class Triagem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string LoteDescarte { get; set; }

        [Required]
        public DateTime DataStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string StatusTriagem { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
         public DateTime DataExpiracao { get; set; }

        public DateTime DataRetirada { get; set; }

      
        [ForeignKey("OperadorId")]
        public Operador Operador { get; set;} 

        [Required]
         public bool Verificada { get; set; }  


    }
}
