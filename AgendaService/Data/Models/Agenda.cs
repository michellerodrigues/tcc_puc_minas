using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaService.Data.Models
{
    public class Agenda
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string LoteDescarte { get; set; }

        [Required]
        public DateTime DataStatus { get; set; }

        [Required]
        public string StatusAgenda { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
         public DateTime DataExpiracao { get; set; }

        public DateTime DataAgenda { get; set; }

      
        [ForeignKey("ResponsavelId")]
        public Responsavel Responsavel { get; set;} 

        [Required]
         public bool Verificada { get; set; }  

    }
}
