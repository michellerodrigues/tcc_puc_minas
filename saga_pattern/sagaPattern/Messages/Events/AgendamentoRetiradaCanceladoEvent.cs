using System;


namespace Agropop.Saga.Messages.Events
{
    public class AgendamentoRetiradaCanceladoEvent:IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataCancelamento { get; set; }
        public AgendamentoRetiradaCanceladoEvent()
        {
            
        }
        public AgendamentoRetiradaCanceladoEvent(Guid id)
        {
            this.Id = id;
            this.DataCancelamento = DateTime.Now;
        }
    }
}