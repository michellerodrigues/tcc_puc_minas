using System;


namespace Agropop.Saga.Messages.Events
{
    public class AgendamentoRetiradaConfirmadaCanceladoEvent:IEvent
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
