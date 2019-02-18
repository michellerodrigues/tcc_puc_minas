
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TriagemService.Data.JoinFacade;

namespace TriagemService.Data.Models
{
    public class Operador
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NomeOperador { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public virtual ICollection<Triagem> Triagem { get; } = new List<Triagem>();
    }
}