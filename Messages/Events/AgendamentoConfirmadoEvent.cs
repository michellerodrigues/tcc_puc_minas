using System;
using NServiceBus;

namespace Messages.Descartes.Events
{
    public class AgendamentoConfirmadoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime ConfirmadoEm { get; set; }
        public AgendamentoConfirmadoEvent()
        {
            
        }
        public AgendamentoConfirmadoEvent(Guid id)
        {
            this.Id = id;
            this.ConfirmadoEm = DateTime.Now;
        }
    }
}
