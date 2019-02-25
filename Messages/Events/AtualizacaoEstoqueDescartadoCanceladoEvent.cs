using System;
using NServiceBus;

namespace Messages.Descartes.Events
{
    public class AtualizacaoEstoqueDescartadoCanceladoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime DataCancelamento { get; set; }
        public AtualizacaoEstoqueDescartadoCanceladoEvent()
        {
            
        }
        public AtualizacaoEstoqueDescartadoCanceladoEvent(Guid id)
        {
            this.Id = id;
            this.DataCancelamento = DateTime.Now;
        }
    }
}
