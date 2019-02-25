using System;
using NServiceBus;

namespace Messages.Descartes.Events
{
    public class AgendamentoCanceladoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime CanceladoEm { get; set; }
        public AgendamentoCanceladoEvent()
        {
            
        }
        public AgendamentoCanceladoEvent(Guid id)
        {
            this.Id = id;
            this.CanceladoEm = DateTime.Now;
        }
    }
}
