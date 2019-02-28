using System;
using NServiceBus;

namespace Messages.Descartes.Events
{
    public class AgendamentoRealizadoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataRegistro { get; set; }

        public string EmailSolicitante { get; set; }
        public AgendamentoRealizadoEvent()
        {
            
        }
        public AgendamentoRealizadoEvent(Guid id, string emailSolicitante)
        {
            this.Id = id;
            this.DataRegistro = DateTime.Now;
            this.EmailSolicitante = emailSolicitante;
        }
    }
}