using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class RetiradaDescarteRealizadaEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataDescarte { get; set; }
        public RetiradaDescarteRealizadaEvent()
        {
            
        }
        public RetiradaDescarteRealizadaEvent(Guid id, DateTime dataDescarte)
        {
            this.Id = id;
            this.DataDescarte = dataDescarte;
        }
    }
}