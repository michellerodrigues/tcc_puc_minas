using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class DescarteRealizadoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataDescarte { get; set; }
        public DescarteRealizadoEvent()
        {
            
        }
        public DescarteRealizadoEvent(Guid id, DateTime dataDescarte)
        {
            this.Id = id;
            this.DataDescarte = dataDescarte;
        }
    }
}