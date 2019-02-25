using System;
using NServiceBus;

namespace Messages.Descartes.Events
{
    public class AgendamentoConfirmadoCanceladoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataCancelamento { get; set; }
        public AgendamentoConfirmadoCanceladoEvent()
        {
            
        }
        public AgendamentoConfirmadoCanceladoEvent(Guid id)
        {
            this.Id = id;
            this.DataCancelamento = DateTime.Now;
        }
    }
}
