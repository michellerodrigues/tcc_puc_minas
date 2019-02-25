using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class DescarteRealizadoCanceladaEvent : ICommand
    {
        public Guid Id { get; set; }
        public DateTime DataCancelamento { get; set; }

        public DescarteRealizadoCanceladaEvent()
        {
            
        }
        public DescarteRealizadoCanceladaEvent(Guid id, DateTime dataCancelamento)
        {
            this.Id = id;
            this.DataCancelamento = DateTime.Now;
        }
    }
}