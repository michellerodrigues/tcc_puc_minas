using System;
using System.ComponentModel.DataAnnotations;

namespace DescarteService.Data.Models
{
    public class AgendamentoDescarte
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public LoteDescarte LoteDescarte { get; set; }

        [Required]
        public DateTime DataRegistro { get; set; }

        public DateTime DataAgendamento { get; set; }

        [Required]
        public string StatusAgendamento { get; set; } = "Pendente";

    }
}
