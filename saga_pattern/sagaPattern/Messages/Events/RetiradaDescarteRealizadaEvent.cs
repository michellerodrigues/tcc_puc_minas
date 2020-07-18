using System;


namespace Agropop.Saga.Messages.Events
{
    public class RetiradaDescarteRealizadaEvent:IEvent
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