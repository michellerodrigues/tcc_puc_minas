using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class RetiradaDescarteCanceladaEvent : ICommand
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