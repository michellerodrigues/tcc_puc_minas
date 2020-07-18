using System;


namespace Agropop.Saga.Messages.Events
{
    public class RetiradaDescarteCanceladaEvent:IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataCancelamento { get; set; }

        public RetiradaDescarteCanceladaEvent()
        {
            
        }
        public RetiradaDescarteCanceladaEvent(Guid id, DateTime dataCancelamento)
        {
            this.Id = id;
            this.DataCancelamento = DateTime.Now;
        }
    }
}