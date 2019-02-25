using System;
using NServiceBus;

namespace Messages.Descartes.Events
{
    public class AgendamentoRealizadoCanceladoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataCancelamento { get; set; }
        public AgendamentoRealizadoCanceladoEvent()
        {
            
        }
        public AgendamentoRealizadoCanceladoEvent(Guid id)
        {
            this.Id = id;
            this.DataCancelamento = DateTime.Now;
        }
    }
}