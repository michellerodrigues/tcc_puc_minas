using System;
using System.ComponentModel.DataAnnotations;

namespace DescarteService.Data.Models
{
    public class AgendamentoDescarteSolicitado
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid LoteDescarteId { get; set; }
        
        public LoteDescarte LoteDescarte { get; set;} 


        [Required]
        public DateTime DataEnvioEmail { get; set; }

        public DateTime DataPropostaAgendamento { get; set; }

        [Required]
        public string StatusProposta { get; set; } = "Enviado";

    }
}
