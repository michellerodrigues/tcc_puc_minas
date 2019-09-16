using System;
using NServiceBus;

namespace Messages.Descartes.Events
{
    public class AgendamentoRetiradaConfirmadaCanceladoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataCancelamento { get; set; }
        public AgendamentoRetiradaConfirmadaCanceladoEvent()
        {
            
        }
        public AgendamentoRetiradaConfirmadaCanceladoEvent(Guid id)
        {
            this.Id = id;
            this.DataCancelamento = DateTime.Now;
        }
    }
}
