using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class RealizarTriagemCanceladaEvent : ICommand
    {
        public Guid Id { get; set; }
        public DateTime DataCancelamento { get; set; }

        public RealizarTriagemCanceladaEvent()
        {
            
        }
        public RealizarTriagemCanceladaEvent(Guid id, DateTime dataCancelamento)
        {
            this.Id = id;
            this.DataCancelamento = DateTime.Now;
        }
    }
}