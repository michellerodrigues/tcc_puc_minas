using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class TriagemFinalizadaCanceladaEvent : IEvent
    {
        public Guid Id { get; set; }

        public string EmailRealizador { get; set; }

        public DateTime DataCancelamento { get; set; }
        public TriagemFinalizadaCanceladaEvent()
        {
            
        }
        public TriagemFinalizadaCanceladaEvent(Guid id, string emailRealizador)
        {
            this.Id = id;
            this.EmailRealizador = emailRealizador;
            this.DataCancelamento = DateTime.Now;
        }
    }
}