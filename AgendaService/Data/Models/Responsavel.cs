
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgendaService.Data.JoinFacade;

namespace AgendaService.Data.Models
{
    public class Responsavel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NomeResponsavel { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public virtual ICollection<Agenda> Agenda { get; } = new List<Agenda>();
    }
}